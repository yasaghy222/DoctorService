using DoctorService.Entities;

namespace MedicalHealthPlus.Interfaces;

public interface IDoctorService
{
	Doctor? Get(Guid id);
	IEnumerable<Doctor> GetAll();

	IEnumerable<Doctor> GetRecommendedDoctors();
}
