using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chipsoft.Assignments.EPDConsole.Models
{
	public class Appointment
	{
		#region Properties

		public Patient? Patient { get; set; }
		public Physician? Physician { get; set; }
		[Key]
		public DateTime Date { get; set; }

		#endregion

		public override string ToString()
		{
			return $"At {Date.TimeOfDay} {Physician?.Name} has an appointment with {Patient?.Name}";
		}
	}
}
