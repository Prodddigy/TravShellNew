using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravShell.Core.Entities;
using TravShell.Core.Entities.ViewModels;

namespace TravShell.App.GoogleAPI;
public interface IGoogleService
{
    Task<TripViewModel> GetCoordinates(TripViewModel model);

    Task<List<Attraction>> GetSetAttractions(string coords, AttractionType type, List<Attraction> list);

    Task<List<Attraction>> GetSetAttractionsDetails(List<Attraction> list);

}

