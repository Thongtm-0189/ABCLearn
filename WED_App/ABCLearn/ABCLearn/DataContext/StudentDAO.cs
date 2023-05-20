using ABCLearn.Models;
using System.Data;

namespace ABCLearn.DataContext
{
    public class StudentDAO
    {
        private static StudentDAO instance;
        public static StudentDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new StudentDAO();
                }
                return instance;
            }
        }

        public bool login(AccountLogin acc)
        {
            string query = "SELECT * FROM tblStudent WHERE Email = @Email AND Password = @pass";
            DataTable tb = ConectionData.ExecuteQuery(query, new object[] { acc.Email, acc.Password });
            foreach (DataRow row in tb.Rows)
            {
                UserLogin obj = new UserLogin()
                {
                    Id = Convert.ToInt32(row["IDStudent"].ToString()),
                    FirstName = row["FirstName"].ToString(),
                    LastName = row["LastName"].ToString(),
                    RoleID = row["RoleID"].ToString(),
                    Password = row["Password"].ToString(),
                    Email = row["Email"].ToString(),
                    Phone = row["Phone"].ToString(),
                    IsConfirmEmail = Boolean.Parse(row["ConfirmEmail"].ToString()),
                    DateCreated = DateTime.Parse(row["DateCreate"].ToString())
                };
                UserLogin.Instance = obj;
            }
            if (tb.Rows.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public List<Course> GetCourses(int id)
        {
            List<Course> _courseStudents = new List<Course>();
            string query = "SELECT * FROM tblCourse , tblCourseOfStudent WHERE tblCourse.IDCourse = tblCourseOfStudent.IDCourse AND IDStudent = @id";
            DataTable dataTable = ConectionData.ExecuteQuery(query, new object[] { id });
            foreach (DataRow row in dataTable.Rows)
            {
                Course obj = new Course()
                {
                    Id = Convert.ToInt32(row["IDCourse"].ToString()),
                    Lecturer = LecturerDAO.Instance().Lecturers().FirstOrDefault(x => x.Id == Convert.ToInt32(row["IDLecturer"])),
                    Title = row["Title"].ToString(),
                    Detail = row["Detail"].ToString(),
                    Price = float.Parse(row["Price"].ToString()),
                    Sale = float.Parse(row["Sale"].ToString())
                };
                _courseStudents.Add(obj);
            }
            return _courseStudents;
        }
    }
}
