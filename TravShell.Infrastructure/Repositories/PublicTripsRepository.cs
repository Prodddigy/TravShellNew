using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravShell.Core.Entities;
using TravShell.Core.Entities.ViewModels;
using TravShell.Core.Repositories;
using TravShell.Infrastructure.Data;

namespace TravShell.Infrastructure.Repositories;
public class PublicTripsRepository : IPublicTripsRepository
{

	private readonly DataContext _dbContext;

	public PublicTripsRepository(DataContext dbContext)
	{
		_dbContext = dbContext;
	}

    public async Task<Trip> CopyTripToUserTrips(int trip, int userId,int newOwnerId, DateTime? newStartDate)
    {
		var tripCheck = await _dbContext.Trips.Where(x => x.IsPublic == true && x.User.UserId == userId && x.TripId == trip)
						.Include(x=> x.Attractions).FirstOrDefaultAsync();

		var newUser = await _dbContext.Users.Where(x => x.UserId == newOwnerId).FirstOrDefaultAsync();
		if (tripCheck != null && newUser != null)
		{
			
			Trip t = new Trip
			{
				City = tripCheck.City,
				Days = tripCheck.Days,
				Description = tripCheck.Description,
				IsPublic = false,
				latitude = tripCheck.latitude,
				longitude = tripCheck.longitude,
				ShortDescription = tripCheck.ShortDescription,
				Name = tripCheck.Name,
				User = newUser,
				Firstdate = newStartDate.Value,
				Lastdate =  newStartDate.Value.AddDays(tripCheck.Days -1),
			};

			await _dbContext.Trips.AddAsync(t);
			//await _dbContext.SaveChangesAsync();	
			List<Attraction> newAtts = new List<Attraction>();
			DateTime tmpDate = tripCheck.Attractions.OrderBy(x => x.StartDate).First().StartDate;
			DateTime newDateToSet = newStartDate.Value;


            foreach (var att in  tripCheck.Attractions.OrderBy(x=>x.StartDate).ToList())
			{
				if(att.StartDate != tmpDate)
				{
					newDateToSet = newDateToSet.AddDays(1);
				tmpDate = 	tmpDate.AddDays(1);
				}

				newAtts.Add(new Attraction
				{
					PlaceCode = att.PlaceCode,
					Name = att.Name,
					Vicinity = att.Vicinity,
					Rating = att.Rating,
					Opening_Hours = att.Opening_Hours,
					Website = att.Website,
					Type = att.Type,
					StartDate = newDateToSet,
					step = att.step,
					latitude = att.latitude,
					longitude = att.longitude,
					Trip = t
				});
			}
			await _dbContext.Attractions.AddRangeAsync(newAtts);

			await _dbContext.SaveChangesAsync();

			return t;

		}


		return null;

    }

    public async Task<int> CountAllTrips()
	{
		return await _dbContext.Trips.Where(x => x.IsPublic == true).CountAsync();
	}

	//public async Task<List<Tag>> GetAllTripTags()
	//{
	//	return await _dbContext.Tags.Distinct().ToListAsync();
	//}

	public async Task<List<Trip>> GetNineTrips(int page, int trips, int orderBy, string name, int searchOption)
	{
		switch (orderBy)
		{
			case 0:
				return await _dbContext.Trips.Where(x=>x.IsPublic==true).Include(x=>x.User).OrderBy(x=>x.TripId).Skip((page - 1) * trips).Take(trips).ToListAsync();
			case 1:
				return await _dbContext.Trips.Where(x => x.IsPublic == true).Include(x => x.User)
							.OrderBy(x => x.Days)
							.Skip((page - 1) * trips).Take(trips).ToListAsync();
			case 2:
				return await _dbContext.Trips.Where(x => x.IsPublic == true).Include(x => x.User)
							.OrderByDescending(x => x.Days)
							.Skip((page - 1) * trips).Take(trips).ToListAsync();
			case 3:
				return await _dbContext.Trips.Where(x => x.IsPublic == true).Include(x => x.User)
							.OrderBy(x => x.Name)
							.Skip((page - 1) * trips).Take(trips).ToListAsync();
			case 4:
				return await _dbContext.Trips.Where(x => x.IsPublic == true).Include(x => x.User)
							.OrderByDescending(x => x.Name)
							.Skip((page - 1) * trips).Take(trips).ToListAsync();
			case 5:

				if (searchOption == 1)
				{
					return await _dbContext.Trips.Where(x => x.IsPublic == true).Include(x => x.User)
						.Where(x => x.Name.ToLower().Contains(name.ToLower()))
						.Skip((page - 1) * trips).Take(trips).ToListAsync();
				}
				else
				{
					return await _dbContext.Trips.Where(x => x.IsPublic == true).Include(x => x.User)
						.Where(x => x.City.ToLower().Contains(name.ToLower()))
						.Skip((page - 1) * trips).Take(trips).ToListAsync();
				}
			case 6:

				return await _dbContext.Trips.Where(x => x.IsPublic == true).Include(x => x.User)
					.Where(x => x.Tags.Any(tag => tag.Name == name)).ToListAsync();

			default:
				return await _dbContext.Trips.Where(x => x.IsPublic == true).Include(x => x.User).Skip((page - 1) * trips).Take(trips).ToListAsync();
		}
	}
}

