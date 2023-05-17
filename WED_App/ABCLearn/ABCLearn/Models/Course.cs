namespace ABCLearn.Models
{
    public class Course
    {
        public int Id { get; set; }
        public int IDLecturer { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public float Price { get; set; }
        public float Sale { get; set; }
        public List<Student> Students { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Calendar> Calendars { get; set; }
    }
}
