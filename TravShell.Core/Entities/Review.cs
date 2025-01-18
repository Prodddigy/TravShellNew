using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravShell.Core.Entities;
public class Review
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int ReviewId { get; set; }

	[MaxLength(50)]
	public string? Content { get; set; }


	[MinLength(0), MaxLength(5)]
	public int StarReview { get; set; } = 0;

	public bool IsEdited { get; set; } = false;


	[ForeignKey("TripId")]
	public required Trip Trip { get; set; }


	[ForeignKey("UserId")]
	public required User User { get; set; }

}
