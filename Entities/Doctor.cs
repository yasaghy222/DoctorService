namespace DoctorService.Entities;

public class Doctor : User
{
	public int MedicalSysCode { get; set; }
	public float Rate { get; set; }
	public int RaterCount { get; set; }
	public string? Content { get; set; }

	public bool HasTelCounseling { get; set; } = false;
	public bool HasTextCounseling { get; set; } = false;
	public bool AcceptVisit { get; set; } = false;

	public Guid? ClinicId { get; set; }
	public Clinic? Clinic { get; set; }

	public Guid SpecialtyId { get; set; }
	public required Specialty Specialty { get; set; }

	public ICollection<OnlinePlan>? OnlinePlans { get; set; }
	public ICollection<VisitPlan>? VisitPlans { get; set; }
}
