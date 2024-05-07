using DoctorService.Entities;
using Microsoft.EntityFrameworkCore;

namespace DoctorService.Data;

public static class ModelBuilderConfig
{
	public static void OnModelCreatingBuilder(this ModelBuilder modelBuilder)
	{

		modelBuilder.Entity<Doctor>(d =>
		{
			d.HasOne(d => d.Clinic)
			  .WithMany(c => c.Doctors)
			  .HasForeignKey(d => d.ClinicId);

			d.Navigation(c => c.Clinic).AutoInclude();


			d.HasOne(d => d.Specialty)
			  .WithMany(s => s.Doctors)
			  .HasForeignKey(d => d.SpecialtyId);

			d.Navigation(c => c.Specialty).AutoInclude();


			d.HasMany(d => d.OnlinePlans)
			  .WithOne(op => op.Doctor)
			  .HasForeignKey(op => op.DoctorId)
			  .OnDelete(DeleteBehavior.Cascade);

			d.Navigation(c => c.OnlinePlans).AutoInclude();


			d.HasMany(d => d.VisitPlans)
			  .WithOne(vp => vp.Doctor)
			  .HasForeignKey(op => op.DoctorId)
			  .OnDelete(DeleteBehavior.Cascade);

			d.Navigation(c => c.VisitPlans).AutoInclude();

		});


		modelBuilder.Entity<Clinic>(c =>
		{
			c.HasOne(c => c.Location)
			  .WithMany(l => l.Clinics)
			  .HasForeignKey(c => c.LocationId);

			c.Navigation(c => c.Location).AutoInclude();
		});

		modelBuilder.Entity<Specialty>(s =>
		{
			s.Navigation(d => d.Doctors).AutoInclude();
		});
	}
}
