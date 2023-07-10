using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chipsoft.Assignments.EPDConsole.Models
{
	public class Physician
	{
		#region Properties

		[Key]
		public string? Name { get; set; }
		public string? Address { get; set; }

		#endregion
	}
}
