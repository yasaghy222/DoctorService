using DoctorService.Entities;
using Microsoft.EntityFrameworkCore;

namespace DoctorService.Data;

public class DoctorServiceContext(DbContextOptions<DoctorServiceContext> options) : DbContext(options)
{
	public DbSet<Doctor> Providers { get; set; }
	public DbSet<Location> Locations { get; set; }
	public DbSet<VisitPlan> VisitPlans { get; set; }
	public DbSet<OnlinePlan> OnlinePlans { get; set; }
	public DbSet<Specialty> Specialties { get; set; }
	public DbSet<Clinic> Clinics { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Doctor>()
		  .HasOne(d => d.Clinic)
		  .WithMany(c => c.Doctors)
		  .HasForeignKey(d => d.ClinicId);

		modelBuilder.Entity<Doctor>()
			.HasOne(d => d.Specialty)
			.WithMany(s => s.Doctors)
			.HasForeignKey(d => d.SpecialtyId);

		modelBuilder.Entity<Doctor>()
			.HasMany(d => d.OnlinePlans)
			.WithOne(op => op.Doctor)
			.HasForeignKey(op => op.DoctorId)
			.OnDelete(DeleteBehavior.Cascade);

		modelBuilder.Entity<Doctor>()
			.HasMany(d => d.VisitPlans)
			.WithOne(vp => vp.Doctor)
			.HasForeignKey(op => op.DoctorId)
			.OnDelete(DeleteBehavior.Cascade);


		modelBuilder.Entity<Clinic>()
			.HasOne(c => c.Location)
			.WithMany(l => l.Clinics)
			.HasForeignKey(c => c.LocationId);
	}
}
