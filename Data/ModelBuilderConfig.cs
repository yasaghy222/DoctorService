using DoctorService.Entities;
using Microsoft.EntityFrameworkCore;

namespace DoctorService.Data;

public static class ModelBuilderConfig
{
	public static void OnModelCreatingBuilder(this ModelBuilder modelBuilder)
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
