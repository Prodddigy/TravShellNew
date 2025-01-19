using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravShell.Core.Entities;

namespace TravShell.App.PublicTrips;
public interface IPublicTripService
{

	Task<int> CountAllTrips();

	public Task<List<Trip>> GetNineTrips( int page, int trips, int orderBy, string name, int searchOption);

	//public Task<Trip> FindTripById(int tripId);

	//public Task<Trip> CopyTripToUserTrips(Trip trip, int userId);

	//public Task<List<Tag>> GetAllTripTags();
}

