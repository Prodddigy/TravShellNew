using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravShell.Core.Entities;
using TravShell.Core.Entities.ViewModels;

namespace TravShell.App.Trips;
public interface ITripService
{
	Task<int> CountUserTrips(int userId);

	public Task<List<Trip>> GetUserTrips(int userId);

	public Task<Trip> FindTripById(int tripId);

	public Task<int> CreateOneDayTrip(TripViewModel model, User user);

	public Task<int> CreateMultiDayTrip(TripViewModel model, User user);

}

