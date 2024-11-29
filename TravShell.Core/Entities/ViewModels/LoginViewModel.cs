using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TravShell.Core.Entities.ViewModels;
public class LoginViewModel
{
	[Required(AllowEmptyStrings = false,ErrorMessage ="Please provide email address")]
	[EmailAddress]
	public string? Email { get; set; }

	[Required(AllowEmptyStrings =false, ErrorMessage ="Please provide password")]
	[PasswordPropertyText]
	public string? Password { get; set; }

}

