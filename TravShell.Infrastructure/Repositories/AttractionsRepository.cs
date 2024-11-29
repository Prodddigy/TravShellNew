using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravShell.Core.Entities;
using TravShell.Core.Repositories;
using TravShell.Infrastructure.Data;

namespace TravShell.Infrastructure.Repositories;
public class AttractionsRepository : IAttractionsRepository
{
	private readonly DataContext _dbContext;
	public AttractionsRepository(DataContext dbContext) 
	{ 
		_dbContext = dbContext;
	}

	public async Task<int> CountTripAttractions(int tripId)
	{
		return await _dbContext.Attractions.CountAsync(x => x.Trip.Id == tripId);
	}

    public async Task<List<Attraction>> GetTripAttractionsOrderByStartDate(int tripId)
    {
        return await _dbContext.Attractions.Where(x => x.Trip.Id == tripId)
                                                .OrderBy(x => x.StartDate)
                                                .ToListAsync();
    }

    public async Task SaveAttractionList(List<Attraction> attractions)
    {
        await _dbContext.Attractions.AddRangeAsync(attractions);
        await _dbContext.SaveChangesAsync();
    }
}

