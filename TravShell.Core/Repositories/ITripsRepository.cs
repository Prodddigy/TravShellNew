using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TravShell.Core.Entities;
using TravShell.Core.Entities.ViewModels;

namespace TravShell.Core.Repositories;
public interface ITripsRepository
{

	public Task<int> CountUserTrips(int userId);

	public Task<List<Trip>> GetUserTrips(int userId);

	public Task<List<Trip>> GetNineUserTrips(int userId, int page, int trips, int orderBy);

	public Task<Trip> FindTripById(int tripId);

	public Task<int> CreateOneDayTrip(TripViewModel model, User user);

	public Task<int> CreateMultiDayTrip(TripViewModel model, User user);

	public Task RemoveTripById(int tripId);

	public Task EditTripDescription(string description, int tripId);
}

