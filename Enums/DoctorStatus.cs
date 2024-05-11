namespace DoctorService;

public enum DoctorStatus : byte
{
	NotConfirmed = 0,
	Confirmed = 1,
	Suspend = 2,
	InCounseling = 3,
}
