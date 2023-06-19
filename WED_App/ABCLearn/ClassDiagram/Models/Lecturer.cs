namespace ABCLearn.Models
{
    public class Lecturer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string RoleID { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Avatar { get; set; }
        public List<Quiz> Quizs { get; set; }
        public List<Course> Courses { get; set; }
    }
}
