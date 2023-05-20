namespace ABCLearn.Models
{
    public class UserLogin
    {
        private static UserLogin instance = new UserLogin();
        public static UserLogin Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UserLogin();
                }
                return instance;
            }
            set { instance = value; }
        }// => instance ?? (instance = new UserLogin());
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string RoleID { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool IsConfirmEmail { get; set; }
        public bool Islogin { get; set; } = false;
        public DateTime DateCreated { get; set; }
        public List<Course> Courses { get; set; }
    }
}
