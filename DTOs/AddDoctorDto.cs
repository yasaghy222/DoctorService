using DoctorService.Enums;

namespace DoctorService.DTOs;

public class AddDoctorDto
{
	public required string Name { get; set; }
	public required string Family { get; set; }
	public required string Phone { get; set; }
	public string? Email { get; set; }
	public GenderType Gender { get; set; } = GenderType.Men;
	public required int MedicalSysCode { get; set; }
	public required Guid SpecialtyId { get; set; }
}
