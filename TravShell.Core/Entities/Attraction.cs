using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TravShell.Core.Entities;
public class Attraction
{
	
	public int Id { get; set; }
	public string PlaceCode { get; set; }

	public string Name { get; set; }

	public string Vicinity { get; set; }

	public string? Rating { get; set; }

	public string? Opening_Hours { get; set; }
	public string? Website { get; set; }

	public AttractionType Type { get; set; } = AttractionType.Attraction;

	public DateTime StartDate { get; set; } = DateTime.Now.Date;
    public double latitude { get; set; }
    public double longitude { get; set; }

    public int step { get; set; } = 0;

	public Trip Trip { get; set; }

}

