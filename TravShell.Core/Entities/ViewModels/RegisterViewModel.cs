using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace TravShell.Core.Entities.ViewModels;
public class RegisterViewModel
{
	[MinLength(2)]
	[Required(AllowEmptyStrings = false, ErrorMessage = "Please provide Forename")]
	public string Forename { get; set; } = string.Empty;

	[Required(AllowEmptyStrings = false, ErrorMessage = "Please provide Surename")]
	public string Surename { get; set; } = string.Empty;

	[Required(AllowEmptyStrings = false, ErrorMessage = "Please provide email address")]
	[EmailAddress]
	public string Email { get; set; } = string.Empty;

	[Required(AllowEmptyStrings = false, ErrorMessage = "Please provide password")]
	[PasswordPropertyText]
	public string Password { get; set; } = string.Empty;

	[Compare(nameof(Password))]
	public string Password2 { get; set; } = string.Empty;

	[Required(AllowEmptyStrings = false, ErrorMessage = "Please provide date of birth")]
	public DateTime? DOB { get; set; } = DateTime.Now;

}

