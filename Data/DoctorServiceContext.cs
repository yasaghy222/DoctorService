using DoctorService.Entities;
using Microsoft.EntityFrameworkCore;

namespace DoctorService.Data;

public class DoctorServiceContext(DbContextOptions<DoctorServiceContext> options) : DbContext(options)
{
	public DbSet<Doctor> Doctors { get; set; }
	public DbSet<Location> Locations { get; set; }
	public DbSet<VisitPlan> VisitPlans { get; set; }
	public DbSet<OnlinePlan> OnlinePlans { get; set; }
	public DbSet<Specialty> Specialties { get; set; }
	public DbSet<Clinic> Clinics { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.OnModelCreatingBuilder();
	}
}
