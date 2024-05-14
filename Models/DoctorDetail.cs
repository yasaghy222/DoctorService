using DoctorService.DTOs;
using DoctorService.Entities;
using DoctorService.Enums;

namespace DoctorService;

public class DoctorDetail : BaseEntity
{
	public required string Name { get; set; }
	public required string Family { get; set; }
	public required string FullName { get; set; }

	public string? Phone { get; set; }
	public bool PhoneValidate { get; set; } = false;

	public string? Email { get; set; }
	public bool EmailValidate { get; set; } = false;

	public GenderType Gender { get; set; } = GenderType.Men;

	public required string ImagePath { get; set; }

	public required int MedicalSysCode { get; set; }
	public string? Content { get; set; }

	public Guid SpecialtyId { get; set; }
	public required string SpecialtyTitle { get; set; }

	public float Rate { get; set; } = 0;
	public int RaterCount { get; set; } = 0;
	public int SuccessConsolationCount { get; set; } = 0;
	public int SuccessReservationCount { get; set; } = 0;

	public bool HasTelCounseling { get; set; } = false;
	public bool HasTextCounseling { get; set; } = false;
	public ICollection<OnlinePlanDto>? OnlinePlans { get; set; }

	public bool AcceptVisit { get; set; } = false;
	public Guid? ClinicId { get; set; }
	public ClinicDto? Clinic { get; set; }
	public ICollection<VisitPlanDto>? VisitPlans { get; set; }
	public DateTime? NearestVisitDate { get; set; } = null;

	public DoctorLineStatus LineStatus { get; set; } = DoctorLineStatus.Offline;
	public DoctorStatus Status { get; set; } = DoctorStatus.NotConfirmed;
}
