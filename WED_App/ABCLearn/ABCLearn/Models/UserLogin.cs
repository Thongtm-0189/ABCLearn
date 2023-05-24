using ABCLearn.DataContext;
using System.Data;

namespace ABCLearn.Models
{
    public class UserLogin : Student
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
        public TimeSpan TimeLogin { get; set; }
        public bool Islogin { get; set; } = false;
        public static void updateIfor(int id, string type)
        {
            string query = "";
            DataTable tb;
            switch (type)
            {
                case "Student":
                    query = "SELECT * FROM tblStudent WHERE IDStudent = @id";
                    tb = ConectionData.ExecuteQuery(query, new object[] { id });
                    foreach (DataRow row in tb.Rows)
                    {
                        UserLogin obj = new UserLogin()
                        {
                            Id = Convert.ToInt32(row["IDStudent"].ToString().Trim()),
                            FirstName = row["FirstName"].ToString().Trim(),
                            LastName = row["LastName"].ToString().Trim(),
                            RoleID = "Student",
                            Password = row["Password"].ToString().Trim(),
                            Email = row["Email"].ToString().Trim(),
                            Phone = row["Phone"].ToString().Trim(),
                            Courses = StudentDAO.Instance.GetCourses(Convert.ToInt32(row["IDStudent"].ToString().Trim())),
                            Gander = row["Gander"].ToString().Trim(),
                            DOB = DateTime.Parse(row["DOB"].ToString().Trim()),
                            Avatar = row["Avatar"].ToString().Trim(),
                            IsConfirmEmail = Boolean.Parse(row["ConfirmEmail"].ToString().Trim()),
                            DateCreated = DateTime.Parse(row["DateCreate"].ToString().Trim())
                        };
                        UserLogin.Instance = obj;
                    }
                    break;
                case "Lecturer":
                    query = "SELECT * FROM tblLecturer WHERE IDLecturer = @id";
                    tb = ConectionData.ExecuteQuery(query, new object[] { id });
                    foreach (DataRow row in tb.Rows)
                    {
                        UserLogin obj = new UserLogin()
                        {
                            Id = Convert.ToInt32(row["IDLecturer"].ToString()),
                            FirstName = row["FirstName"].ToString(),
                            LastName = row["LastName"].ToString(),
                            RoleID = "Lecturer",
                            Avatar = row["Avatar"].ToString().Trim(),
                            Password = row["Password"].ToString(),
                            Courses = LecturerDAO.Instance.getCourse(Convert.ToInt32(row["IDLecturer"].ToString())),
                            Email = row["Email"].ToString(),
                            Phone = row["Phone"].ToString()
                        };
                        UserLogin.Instance = obj;
                    }
                    break;
            }
        }
    }
}
