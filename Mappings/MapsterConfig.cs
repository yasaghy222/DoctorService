using System.Reflection;
using DoctorService.DTOs;
using DoctorService.Entities;
using DoctorService.Models;
using Mapster;
namespace DoctorService.Mappings;

public static class MapsterConfig
{
	public static void RegisterMapsterConfiguration(this IServiceCollection services)
	{
		TypeAdapterConfig<Specialty, SpecialtyDto>.NewConfig().Map(dto => dto.DoctorIds,
			s => s.Doctors == null ? null : s.Doctors.Select(d => d.Id).ToArray());

		TypeAdapterConfig<Doctor, DoctorInfo>.NewConfig().Map(dto => dto.SpecialtyTitle,
			s => s.Specialty.Title);


		TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
	}
}
