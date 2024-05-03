using DoctorService.Enums;

namespace DoctorService.Entities
{
    public class OnlinePlan : BaseEntity
    {
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public DayOfWeeks DayOfWeek { get; set; }

        public Guid DoctorId { get; set; }
        public required Doctor Doctor { get; set; }
    }
}