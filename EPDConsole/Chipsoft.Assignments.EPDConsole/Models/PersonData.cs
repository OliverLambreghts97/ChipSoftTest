using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chipsoft.Assignments.EPDConsole.Models
{
	// Tried using this struct following the design principle:
	// "Favor composition over inheritance". When trying to use
	// this non-pod struct composed inside of the Physician & 
	// Patient classes, the compiler complained that EF is
	// unable to use this type of data to map to a database apparently?
	public struct PersonData
	{
		#region Properties

		public string Name { get; set; }
		public string Address { get; set; }

		#endregion
	}
}
