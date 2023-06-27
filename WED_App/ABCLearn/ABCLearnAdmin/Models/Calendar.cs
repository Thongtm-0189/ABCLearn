using System.Timers;

namespace ABCLearn.Models
{
    public class Calendar
    {
        public int Id { get; set; }
        public int IDCourse { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
