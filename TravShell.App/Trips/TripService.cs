using GoogleApi.Entities.Search.Video.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravShell.Core.Entities;
using TravShell.Core.Entities.ViewModels;
using TravShell.Core.Repositories;

namespace TravShell.App.Trips;
public class TripService : ITripService
{

	private readonly ITripsRepository _tripsRepository;

	public TripService(ITripsRepository tripsRepository) 
	{
		_tripsRepository = tripsRepository;
	}

	public async Task<int> CountUserTrips(int userId)
	{
		return  await _tripsRepository.CountUserTrips(userId);
	}

	public async Task<List<Trip>> GetUserTrips(int userId)
	{
		return await _tripsRepository.GetUserTrips(userId);
	}

    public async Task<Trip> FindTripById(int tripId)
    {
		return await _tripsRepository.FindTripById(tripId);
    }

	public async Task<int> CreateOneDayTrip(TripViewModel model, User user)
	{
		return await _tripsRepository.CreateOneDayTrip(model, user);
	}

	public async Task<int> CreateMultiDayTrip(TripViewModel model, User user)
	{
		return await _tripsRepository.CreateMultiDayTrip(model, user);
	}

   public async Task RemoveTripById(int tripId)
    {
		 await _tripsRepository.RemoveTripById(tripId);
		return;
    }

	public async Task<List<Trip>> GetNineUserTrips(int userId, int page, int trips, int orderBy)
	{
		return await _tripsRepository.GetNineUserTrips(userId, page, trips,  orderBy);
	}

    public async Task EditTripDescription(string description, int tripId)
    {
        await _tripsRepository.EditTripDescription(description, tripId);
    }
}

