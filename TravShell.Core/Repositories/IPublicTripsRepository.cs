using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravShell.Core.Entities;

namespace TravShell.Core.Repositories;
public interface IPublicTripsRepository
{
	Task<int> CountAllTrips();

	public Task<List<Trip>> GetNineTrips(int page, int trips, int orderBy, string name, int searchOption);

	//public Task<List<Tag>> GetAllTripTags();

}

