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
		return  await _dbContext.Trips.CountAsync(x => x.User.Id == userId);
	}

	public async Task<List<Trip>> GetUserTrips(int userId)
	{
		return await _dbContext.Trips.Where(x => x.User.Id == userId).ToListAsync();
	}

    public async Task<Trip?> FindTripById(int tripId)
    {
        return await _dbContext.Trips.Where(x => x.Id == tripId).FirstOrDefaultAsync();
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
		Console.WriteLine(trip.Id + " identyfiacja!!!!");
		await _dbContext.Trips.AddAsync(trip);

		await _dbContext.SaveChangesAsync();
		return trip.Id;
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

		return trip.Id;
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

	public async Task<List<Trip>> GetNineUserTrips(int userId, int page, int trips)
	{
		return await _dbContext.Trips.Where(x=>x.User.Id==userId).Skip((page-1)*trips).Take(trips).ToListAsync();
	}

    public async Task EditTripDescription(string description, int tripId)
    {
		var tripToedit = await _dbContext.Trips.Where(x => x.Id == tripId).FirstOrDefaultAsync();

		if(tripToedit != null)
		{
			tripToedit.Description = description;
            await _dbContext.SaveChangesAsync();
        }
    }
}
