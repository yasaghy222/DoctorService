
using DoctorService.Enums;

namespace DoctorService.Entities;

public class User : BaseEntity
{
	public string Name { get; set; } = "";
	public string Family { get; set; } = "";
	public required string Username { get; set; }
	public string? Phone { get; set; }
	public string? Email { get; set; }
	public GenderType Gender { get; set; } = GenderType.Men;
}


