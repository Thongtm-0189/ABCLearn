namespace ABCLearn.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public int LastName { get; set; }
        public string RoleID { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool IsConfirmEmail { get; set; }
        public DateTime DateCreated { get; set; }
        public List<Course> Courses { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
