using DoctorService.Entities;

namespace DoctorService;

public interface ISpecialtyService
{
	IEnumerable<Specialty> GetAll();
}
