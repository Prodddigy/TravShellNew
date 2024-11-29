using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravShell.Core.Entities;
public class User
{

	public int Id { get; set; }
	
	public string Forename { get; set; } = string.Empty;
	
	public string Surename { get; set; } =string.Empty;

	public string Email { get; set; } =string.Empty ;
	
	public string PasswordHash { get; set; } = string .Empty ;
	
	public DateTime DOB {  get; set; } = DateTime.Now;

	public List<Trip> TripList { get; set; }

}

