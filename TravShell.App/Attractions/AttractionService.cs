using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravShell.Core.Entities;
using TravShell.Core.Entities.ViewModels;
using TravShell.Core.Repositories;
using TravShell.Infrastructure.Data;

namespace TravShell.App.Attractions;
public class AttractionService :IAttractionService
{
	private readonly IAttractionsRepository _attractionsRepository;
	public AttractionService(IAttractionsRepository attractionsRepository)
	{
		_attractionsRepository = attractionsRepository;
	}

	public async Task<int> CountTripAttractions(int tripId)
	{
		return await _attractionsRepository.CountTripAttractions(tripId);
	}

   public async Task<List<Attraction>> GetTripAttractionsOrderByStartDate(int tripId)
    {
        return await _attractionsRepository.GetTripAttractionsOrderByStartDate(tripId);
    }

    public async Task RemoveAttractions(List<Attraction> attractions)
    {
         await _attractionsRepository.RemoveAttractions(attractions);
    }

    public async Task SaveAttractionList(List<AttractionViewModel> attractions)
    {
         await _attractionsRepository.SaveAttractionList(attractions);
    }
}

