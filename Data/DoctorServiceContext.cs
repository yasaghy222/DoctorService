using DoctorService.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace DoctorService.Data;

public class DoctorServiceContext : DbContext
{
	public DbSet<Doctor> Doctors { get; set; }
	public DbSet<Location> Locations { get; set; }
	public DbSet<VisitPlan> VisitPlans { get; set; }
	public DbSet<OnlinePlan> OnlinePlans { get; set; }
	public DbSet<Specialty> Specialties { get; set; }
	public DbSet<Clinic> Clinics { get; set; }

	public DoctorServiceContext(DbContextOptions<DoctorServiceContext> options) : base(options)
	{
		try
		{
			if (Database.GetService<IDatabaseCreator>() is RelationalDatabaseCreator dbCreator)
			{
				if (!dbCreator.CanConnect()) dbCreator.Create();
				if (!dbCreator.HasTables()) dbCreator.CreateTables();
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.Message);
		}
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.OnModelCreatingBuilder();
	}
}
