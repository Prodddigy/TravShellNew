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
   public class TripsRepository : ITripsRepository
    {
        private readonly DataContext _dbContext;

        public TripsRepository(DataContext dbContext) 
        {
            _dbContext = dbContext;    
        }

	public async Task<int> CountUserTrips(int userId)
	{
		return  await _dbContext.Trips.CountAsync(x => x.User.UserId == userId);
	}

	public async Task<List<Trip>> GetUserTrips(int userId)
	{
		return await _dbContext.Trips.Where(x => x.User.UserId == userId).ToListAsync();
	}

    public async Task<Trip?> FindTripById(int tripId)
    {
        return await _dbContext.Trips.Include(x=>x.User).Where(x => x.TripId == tripId).FirstOrDefaultAsync();
    }

	public async Task<int> CreateOneDayTrip(TripViewModel Model, User user)
	{
		Trip trip = new Trip
		{
			Name = Model.Name,
			Firstdate = (DateTime) Model.Firstdate,
			Lastdate =(DateTime) Model.Lastdate,
			Days = 1,
			City = Model.City,
			ShortDescription = Model.ShortDescription,
			User = user,
			latitude = Model.latitude,
			longitude = Model.longitude,
			IsPublic = Model.IsPublic
			
		};
		Console.WriteLine(trip.TripId + " identyfiacja!!!!");
		await _dbContext.Trips.AddAsync(trip);

		await _dbContext.SaveChangesAsync();
		return trip.TripId;
	}

	public async Task<int> CreateMultiDayTrip(TripViewModel Model, User user)
	{
		Trip trip = new Trip
		{
			Name = Model.Name,
			Firstdate =(DateTime) Model.Firstdate,
			Lastdate = (DateTime) Model.Lastdate,
			Days = ((DateTime)Model.Lastdate - (DateTime)Model.Firstdate).Days + 1,
			City = Model.City,
			ShortDescription = Model.ShortDescription,
			User = user,
			latitude = Model.latitude,
			longitude = Model.longitude,
			IsPublic = Model.IsPublic
		};
		await _dbContext.Trips.AddAsync(trip);
		await _dbContext.SaveChangesAsync();

		return trip.TripId;
	}

	public async Task RemoveTripById(int tripId)
    {
       var trip =  await FindTripById(tripId);

        if (trip != null)
		{
			 _dbContext.Trips.Remove(trip);
			await _dbContext.SaveChangesAsync();
		}
		return;
    }

	public async Task<List<Trip>> GetNineUserTrips(int userId, int page, int trips, int orderBy, string name, int searchOption)
	{	
		List<TripViewModel> result = new List<TripViewModel>();

		switch(orderBy)
		{
			case 0:
				return await _dbContext.Trips.Where(x => x.User.UserId == userId).Skip((page - 1) * trips).Take(trips).ToListAsync();
			case 1:

				return await _dbContext.Trips.Where(x => x.User.UserId == userId)
							.OrderBy(x => x.Firstdate)
							.Skip((page - 1) * trips).Take(trips).ToListAsync();
			case 2:
				return await _dbContext.Trips.Where(x => x.User.UserId == userId)
							.OrderByDescending(x => x.Firstdate)
							.Skip((page - 1) * trips).Take(trips).ToListAsync();
			case 3:
				return await _dbContext.Trips.Where(x=> x.User.UserId == userId)
							.OrderBy(x=>x.Days)
							.Skip((page - 1) * trips).Take(trips).ToListAsync();
			case 4:
				return await _dbContext.Trips.Where(x => x.User.UserId == userId)
							.OrderByDescending(x => x.Days)
							.Skip((page - 1) * trips).Take(trips).ToListAsync();
			case 5:
				return await _dbContext.Trips.Where(x => x.User.UserId == userId)
							.OrderBy(x => x.Name)
							.Skip((page - 1) * trips).Take(trips).ToListAsync();
			case 6:
				return await _dbContext.Trips.Where(x => x.User.UserId == userId)
							.OrderByDescending(x => x.Name)
							.Skip((page - 1) * trips).Take(trips).ToListAsync();
			case 7:

				if (searchOption == 1)
				{
					return await _dbContext.Trips
						.Where(x => x.Name.ToLower().Contains(name.ToLower()) && x.User.UserId == userId)
						.Skip((page - 1) * trips).Take(trips).ToListAsync();
				}
				else
				{
					return await _dbContext.Trips
						.Where(x => x.City.ToLower().Contains(name.ToLower()) && x.User.UserId == userId)
						.Skip((page - 1) * trips).Take(trips).ToListAsync();
				}
			case 8:

				return await _dbContext.Trips.Where(x => x.User.UserId == userId)
					.Where(x => x.Tags.Any(tag => tag.Name == name)).ToListAsync();
					
			default:
				return await _dbContext.Trips.Where(x => x.User.UserId == userId).Skip((page - 1) * trips).Take(trips).ToListAsync();
		}
	}

	public async Task<List<Trip>> SearchTripsByProvidedName(string name, int Id)
	{
		return await _dbContext.Trips
			.Where(x => x.Name.Contains(name, StringComparison.OrdinalIgnoreCase) && x.TripId ==Id ).ToListAsync();
	}

	public async Task EditTripDescription(string description, int tripId)
    {
		var tripToedit = await _dbContext.Trips.Where(x => x.TripId == tripId).FirstOrDefaultAsync();

		if(tripToedit != null)
		{
			tripToedit.Description = description;
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task ChangeTripVisibility(bool isPublic, int tripId)
	{
		var trip = await _dbContext.Trips.Where(x=> x.TripId== tripId).FirstOrDefaultAsync();

		if (trip != null)
		{
			trip.IsPublic = isPublic;
		}

		await _dbContext.SaveChangesAsync();

	}

}
