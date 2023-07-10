using Chipsoft.Assignments.EPDConsole.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chipsoft.Assignments.EPDConsole.Data
{
	public class EPDDbContext : DbContext
	{
		// The following configures EF to create a Sqlite database file in the
		protected override void OnConfiguring(DbContextOptionsBuilder options)
			=> options.UseSqlite($"Data Source=edp.db");

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Patient>().ToTable("Patients");
			modelBuilder.Entity<Physician>().ToTable("Physician");
			modelBuilder.Entity<Appointment>().ToTable("Appointment");

		}

		public DbSet<Patient> Patients { get; set; }
		public DbSet<Physician> Physicians { get; set; }
		public DbSet<Appointment> Appointments { get; set; }
	}
}
