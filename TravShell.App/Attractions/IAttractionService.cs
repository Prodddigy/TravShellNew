using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravShell.Core.Entities;
using TravShell.Core.Entities.ViewModels;

namespace TravShell.App.Attractions;
public interface IAttractionService
{
	Task<int> CountTripAttractions(int tripId);
	Task<List<Attraction>> GetTripAttractionsOrderByStartDate(int tripId);

	Task SaveAttractionList(List<AttractionViewModel> attractions);

	Task RemoveAttractions(List<Attraction> attractions);	
}

