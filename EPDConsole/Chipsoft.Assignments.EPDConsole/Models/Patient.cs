using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chipsoft.Assignments.EPDConsole.Models
{
	public class Patient
	{
		#region Properties

		// Wanted to move the underneath shared data to a
		// separate base class from which both Patient &
		// Physician classes could inherit, but I then received errors
		// that I couldn't add a key to a derived class.
		// Removing the key and adding it to the base class,
		// made it so that my Physician & Patient tables were
		// incomplete in the database. Feedback on how this is solved would be greatly
		// appreciated!
		[Key]
		public string? Name { get; set; }
		public string? Address { get; set; }
		public string? MailAddress { get; set; }
		public string? PhoneNumber { get; set; }

		#endregion
	}
}
