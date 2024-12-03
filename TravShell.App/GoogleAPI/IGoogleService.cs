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

    Task<List<AttractionViewModel>> GetSetAttractions(string coords, AttractionType type, List<AttractionViewModel> list);

    Task<List<AttractionViewModel>> GetSetAttractionsDetails(List<AttractionViewModel> list);

}

