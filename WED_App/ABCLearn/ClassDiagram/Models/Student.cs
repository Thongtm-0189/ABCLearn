namespace ABCLearn.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string RoleID { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Gander { get; set; }
        public DateTime DOB { get; set; }
        public bool IsConfirmEmail { get; set; }
        public DateTime DateCreated { get; set; }
        public List<Course> Courses { get; set; }
        public List<Comment> Comments { get; set; }
        public string Avatar { get; set; }
        public Student() { }
        public Student(int id, string firstName, string lastName, string roleID, string password, string email, string phone, string gander, DateTime dOB, bool isConfirmEmail, DateTime dateCreated, List<Course> courses, List<Comment> comments, string avatar)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            RoleID = roleID;
            Password = password;
            Email = email;
            Phone = phone;
            Gander = gander;
            DOB = dOB;
            IsConfirmEmail = isConfirmEmail;
            DateCreated = dateCreated;
            Courses = courses;
            Comments = comments;
            Avatar = avatar;
        }
    }
}
