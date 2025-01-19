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

