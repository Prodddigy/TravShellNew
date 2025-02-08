using System.ComponentModel.DataAnnotations;

namespace TravShell.Core.Entities.ViewModels
{
    public class TripViewModel
    {

		public int Id { get; set; }
		//[MinLength(6)]
		//[Required(AllowEmptyStrings = false, ErrorMessage = "Please provide Name for the Trip, try\" My own Cool trip\"")]
		[Required]
		[StringLength(20, ErrorMessage = "Min. 6 & max 20 characters, try \" My own Cool trip\"", MinimumLength = 6)]
        public string Name { get; set; }
        public int Days { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide first day")]
        public DateTime? Firstdate { get; set; } 

		[Required(AllowEmptyStrings = false, ErrorMessage = "Please provide last day or just set the same day as first day")]
        public DateTime? Lastdate { get; set; } 

		[Required(AllowEmptyStrings = false, ErrorMessage = "Please provide city for the trip")]
        public string City { get; set; }

        public double latitude { get; set; }
        public double longitude { get; set; }


		
		[StringLength(40, ErrorMessage = "Short desc. must be at least 10 characters long.", MinimumLength = 10)]
		public string ShortDescription { get; set; } = "";

		public string Description { get; set; } = "";

		[Required]
        public Boolean IsPublic { get; set; } 


  //      public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		//{
		//	if (Lastdate <= Firstdate)
		//	{
		//		yield return new ValidationResult("End date must be greater than the start date.", new[] { "Lastdate" });
		//	}
		//}

	}
}
