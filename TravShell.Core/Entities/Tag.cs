using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravShell.Core.Entities;
public class Tag
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int TagId { get; set; }

	[Required]
	[MaxLength(10)]
	public string Name { get; set; }
	[Required]
	public int AuthorId { get; set; }


}

