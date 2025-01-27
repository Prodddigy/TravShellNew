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
public class AttractionsRepository : IAttractionsRepository
{
	private readonly DataContext _dbContext;
	public AttractionsRepository(DataContext dbContext) 
	{ 
		_dbContext = dbContext;
	}

	public async Task<int> CountTripAttractions(int tripId)
	{
		return await _dbContext.Attractions.CountAsync(x => x.Trip.TripId == tripId);
	}

    public async Task<List<Attraction>> GetTripAttractionsOrderByStartDate(int tripId)
    {
        return await _dbContext.Attractions.Where(x => x.Trip.TripId == tripId)
                                                .OrderBy(x => x.StartDate)
                                                .ToListAsync();
    }

    public async Task RemoveAttractions(List<Attraction> attractions)
    {
        if(attractions != null || attractions.Count() >0)
        {
             _dbContext.Attractions.RemoveRange(attractions);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task SaveAttractionList(List<AttractionViewModel> attractions)
    {List<Attraction> newList= new List<Attraction>();

        foreach (var att in attractions)
        {
            newList.Add(new Attraction
            {
                PlaceCode = att.PlaceCode,
                Name = att.Name,
                Vicinity = att.Vicinity,
                Rating = att.Rating,
                Opening_Hours = att.Opening_Hours,
                Website = att.Website,
                Type = att.Type,
                StartDate = att.StartDate,
                step = att.step,
                latitude = att.latitude,
                longitude = att.longitude,
                Trip = att.Trip
            });
        }

        await _dbContext.Attractions.AddRangeAsync(newList);
        await _dbContext.SaveChangesAsync();
    }
}

