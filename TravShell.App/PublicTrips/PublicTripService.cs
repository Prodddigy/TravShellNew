using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravShell.Core.Entities;
using TravShell.Core.Repositories;

namespace TravShell.App.PublicTrips;
public class PublicTripService : IPublicTripService
{

	private readonly IPublicTripsRepository _publicTripsRepository;

	public PublicTripService(IPublicTripsRepository publicTripsRepository)
	{
		_publicTripsRepository = publicTripsRepository;
	}

	//public Task<Trip> CopyTripToUserTrips(Trip trip, int userId)
	//{
	//	throw new NotImplementedException();
	//}

	public async Task<int> CountAllTrips()
	{
		return await _publicTripsRepository.CountAllTrips();
	}

	//public Task<Trip> FindTripById(int tripId)
	//{
	//	throw new NotImplementedException();
	//}



	public async Task<List<Trip>> GetNineTrips(int page, int trips, int orderBy, string name, int searchOption)
	{
		return await _publicTripsRepository.GetNineTrips(page, trips, orderBy, name, searchOption);
	}
}

