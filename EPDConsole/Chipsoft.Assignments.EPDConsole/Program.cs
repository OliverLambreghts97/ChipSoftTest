using System.Globalization;
using System.Text.RegularExpressions;
using Chipsoft.Assignments.EPDConsole.Data;
using Chipsoft.Assignments.EPDConsole.Models;
using Microsoft.EntityFrameworkCore;

namespace Chipsoft.Assignments.EPDConsole
{
	public class Program
	{
		//Don't create EF migrations, use the reset db option
		//This deletes and recreates the db, this makes sure all tables exist

		private static EPDDbContext context = new EPDDbContext();
		private const string ReturnString = "Quit";

		private static void AddPatient()
		{
			var newPatient = new Patient();

			Console.WriteLine("Enter patient name: ");
			newPatient.Name = Console.ReadLine();

			Console.WriteLine("Enter patient's Address: ");
			newPatient.Address = Console.ReadLine();

			Console.WriteLine("Enter patient's mail adress: ");
			newPatient.MailAddress = Console.ReadLine();

			Regex regex = new Regex("^\\+?\\d+$");
			string? phoneNumber = string.Empty;

			while (phoneNumber != null && regex.IsMatch(phoneNumber) == false)
			{
				Console.WriteLine("Enter patient's phone number: ");
				phoneNumber = Console.ReadLine();
			}
			newPatient.PhoneNumber = phoneNumber;

			if (context.Patients.FirstOrDefault(p => p.Name == newPatient.Name) != null)
			{
				Console.WriteLine("Patient already exists in database, press a key to return");
				Console.ReadKey();
				return;
			}

			context.Patients.Add(newPatient);
			var entries = context.SaveChanges();
		}

		private static void ShowAppointment()
		{
			DateTime dateTime;
			Console.WriteLine("Enter appointment date (YYYY-MM-DD): ");
			string? input = Console.ReadLine();
			if (DateTime.TryParse(input, out dateTime))
			{
				// Extract the date portion from the DateTime object
				DateTime dateOnly = dateTime.Date;

				var appointments = context.Appointments
					.Where(a => a.Date.Date == dateOnly)
					.ToList();

				if (appointments.Count == 0)
					Console.WriteLine("No appointments found for this day, press a key to continue");

				foreach (var appointment in appointments)
				{
					Console.WriteLine(appointment);
				}
			}
			else
			{
				Console.WriteLine("Invalid date format, press a key to continue");
			}

			Console.ReadKey();
		}

		private static void AddAppointment()
		{
			var newAppointment = new Appointment();

			// Fetch physician by name
			Physician? physician = null;

			while (physician == null)
			{
				Console.WriteLine($"Enter physician's name or type '{ReturnString}' to return: ");
				string? physicianName = Console.ReadLine();
				if (physicianName == ReturnString)
					return;
				physician = context.Physicians.FirstOrDefault(p => p.Name == physicianName);
				if (physician == null) Console.WriteLine("Physician not found");
			}
			newAppointment.Physician = physician;

			// Fetch patient by name
			Patient? patient = null;

			while (patient == null)
			{
				Console.WriteLine($"Enter patient's name or type '{ReturnString}' to return: ");
				string? patientName = Console.ReadLine();
				if (patientName == ReturnString)
					return;
				patient = context.Patients.FirstOrDefault(p => p.Name == patientName);
				if (patient == null) Console.WriteLine("Patient not found");
			}
			newAppointment.Patient = patient;

			// Set date of the appointment
			string? dateInput = string.Empty;
			DateTime correctDateData = DateTime.Now;

			while (DateTime.TryParseExact(dateInput, "yyyy-MM-dd HH:mm", null, DateTimeStyles.None,
					   out DateTime appointmentDate) == false)
			{
				Console.WriteLine($"Enter appointment date (YYYY-MM-DD HH:mm) or type '{ReturnString}' to return: ");
				dateInput = Console.ReadLine();
				if (dateInput == ReturnString)
					return;
				if (DateTime.TryParseExact(dateInput, "yyyy-MM-dd HH:mm", null, DateTimeStyles.None, out
						correctDateData) == false)
					Console.WriteLine("Invalid date format");
			}
			newAppointment.Date = correctDateData;

			// Add the appointment to the database
			context.Appointments.Add(newAppointment);
			context.SaveChanges();
		}

		private static void DeletePhysician()
		{
			Physician? toDelete = null;

			while (toDelete == null)
			{
				Console.WriteLine($"Enter physician's name or type '{ReturnString}' to return: ");
				string? name = Console.ReadLine();
				if (name == ReturnString) return;

				toDelete = context.Physicians.FirstOrDefault(p => p.Name != null && p.Name.Equals(name));
				if (toDelete == null) Console.WriteLine("Physician not found");
			}

			context.Physicians.Remove(toDelete);
			context.SaveChanges();
		}

		private static void AddPhysician()
		{
			var newPhysician = new Physician();

			Console.WriteLine("Enter physician's name: ");
			newPhysician.Name = Console.ReadLine();

			Console.WriteLine("Enter physician's Address: ");
			newPhysician.Address = Console.ReadLine();

			if (context.Physicians.FirstOrDefault(p => p.Name == newPhysician.Name) != null)
			{
				Console.WriteLine("Physician already exists in database, press a key to return");
				Console.ReadKey();
				return;
			}

			context.Physicians.Add(newPhysician);
			var entries = context.SaveChanges();
		}

		private static void DeletePatient()
		{
			Patient? toDelete = null;

			while (toDelete == null)
			{
				Console.WriteLine($"Enter patient's name or type '{ReturnString}' to return: ");
				string? name = Console.ReadLine();
				if (name == ReturnString) return;
				toDelete = context.Patients.FirstOrDefault(p => p.Name != null && p.Name.Equals(name));
				if (toDelete == null) Console.WriteLine("Patient not found");
			}

			context.Patients.Remove(toDelete);
			context.SaveChanges();
		}

		#region FreeCodeForAssignment

		static void Main(string[] args)
		{
			LoadDatabase();
			while (ShowMenu())
			{
				//Continue
			}
		}

		static void LoadDatabase()
		{
			context.Patients.Load();
			context.Physicians.Load();
			context.Appointments.Load();
		}

		static void ResetDB()
		{
			context.Database.EnsureDeleted();
			context.Database.EnsureCreated();
		}

		public static bool ShowMenu()
		{
			Console.Clear();
			foreach (var line in File.ReadAllLines("logo.txt"))
			{
				Console.WriteLine(line);
			}
			Console.WriteLine("");
			Console.WriteLine("1 - Patient toevoegen");
			Console.WriteLine("2 - Patienten verwijderen");
			Console.WriteLine("3 - Arts toevoegen");
			Console.WriteLine("4 - Arts verwijderen");
			Console.WriteLine("5 - Afspraak toevoegen");
			Console.WriteLine("6 - Afspraken inzien");
			Console.WriteLine("7 - Sluiten");
			Console.WriteLine("8 - Reset db");

			if (int.TryParse(Console.ReadLine(), out int option))
			{
				switch (option)
				{
					case 1:
						AddPatient();
						return true;
					case 2:
						DeletePatient();
						return true;
					case 3:
						AddPhysician();
						return true;
					case 4:
						DeletePhysician();
						return true;
					case 5:
						AddAppointment();
						return true;
					case 6:
						ShowAppointment();
						return true;
					case 7:
						return false;
					case 8:
						ResetDB();
						return true;
					default:
						return true;
				}
			}
			return true;
		}

		#endregion
	}
}