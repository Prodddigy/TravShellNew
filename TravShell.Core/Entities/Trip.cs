using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TravShell.Core.Entities;
public class Trip
{
	[Key]
	public int TripId { get; set; }
	
	[MinLength(6)]
	[Required(AllowEmptyStrings = false, ErrorMessage = "Please provide Name for the Trip, try\" My own Cool trip\"")]
	
	public string Name { get; set; }

	public string ShortDescription { get; set; } = "";

	public string Description { get; set; } = "";


    public int Days { get; set; }
	[Required(AllowEmptyStrings = false, ErrorMessage = "Please provide first day")]
	public DateTime Firstdate { get; set; } = DateTime.Now;
	[Required(AllowEmptyStrings = false, ErrorMessage = "Please provide last day or just set the same day as first day")]
	public DateTime Lastdate { get; set; } = DateTime.Now;

    [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide city for the trip")]
    public string City { get; set; }

    public double latitude { get; set; }
    public double longitude { get; set; }	

	[Required]
	public Boolean IsPublic { get; set; } 

	public User User { get; set; }

	public List<Attraction> Attractions { get; set; }


	public List<Review> Reviews { get; set; } = new List<Review>();

	public List<Tag> Tags { get; set; }



}

