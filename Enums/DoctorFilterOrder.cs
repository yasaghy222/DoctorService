namespace DoctorService.Enums;

public enum DoctorFilterOrder : byte
{
	Default = 0,
	Rate = 1,
	NearestReservationTime = 2,
	SuccessReservationCount = 3,
}
