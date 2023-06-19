namespace ABCLearn.Models
{
    public class Profile
    {
        private static Profile _instance;
        public static Profile Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Profile();
                return _instance;
            }
            set
            {
                _instance = value;
            }
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gander { get; set; }
        public DateTime DOB { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Role { get; set; }
    }
}
