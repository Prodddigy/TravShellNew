using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorBootstrap;
namespace TravShell.Core.Entities.ViewModels;
public class AttractionViewModel
{

    public int Id { get; set; }
    public string PlaceCode { get; set; }

    public string Name { get; set; }

    public string Vicinity { get; set; }

    public string? Rating { get; set; }

    public string? Opening_Hours { get; set; }
    public string? Website { get; set; }

    public AttractionType Type { get; set; }

    public DateTime StartDate { get; set; } = DateTime.Now.Date;
    public double latitude { get; set; }
    public double longitude { get; set; }

    public int step { get; set; } = 0;
    
    public string dropzone { get; set; }

    public Trip Trip { get; set; }

    public GoogleMapMarker Marker { get; set; }


}

