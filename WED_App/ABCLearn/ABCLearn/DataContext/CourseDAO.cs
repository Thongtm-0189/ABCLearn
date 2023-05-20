using ABCLearn.Models;
using System.Data;
using System.Globalization;

namespace ABCLearn.DataContext
{
    public class CourseDAO
    {
        private List<Course> _coureses = new List<Course>();
        private static CourseDAO insctance;
        public static CourseDAO Instance()
        {
            if (insctance == null)
            {
                insctance = new CourseDAO();
            }
            return insctance;
        }

        public List<Course> Courses()
        {
            if (_coureses.Count == 0)
            {
                getCourse();
            }
            return _coureses;
        }
        public void getCourse()
        {
            if (_coureses.Count == 0)
            {
                string query = "SELECT * FROM tblCourse";
                DataTable dataTable = ConectionData.ExecuteQuery(query);
                foreach (DataRow row in dataTable.Rows)
                {
                    Course obj = new Course()
                    {
                        Id = Convert.ToInt32(row["IDCourse"].ToString()),
                        Lecturer = LecturerDAO.Instance().Lecturers().FirstOrDefault(x => x.Id == Convert.ToInt32(row["IDLecturer"])),
                        Title = row["Title"].ToString(),
                        Detail = row["Detail"].ToString(),
                        Status = bool.Parse(row["Status"].ToString()),
                        Price = float.Parse(row["Price"].ToString()),
                        Sale = float.Parse(row["Sale"].ToString())
                    };

                    _coureses.Add(obj);
                }
            }
        }
        public List<Comment> getComment(int id)
        {
            List<Comment> comments = new List<Comment>();
            string query = "SELECT * FROM tblCommentOfStudent, tblStudent WHERE tblStudent.IDStudent = tblCommentOfStudent.IDStudent AND IDCourse = @id";
            DataTable dataTable = ConectionData.ExecuteQuery(query, new object[] { id });
            foreach (DataRow row in dataTable.Rows)
            {
                Comment obj = new Comment()
                {
                    StudentName = $"{row["FirstName"].ToString()} {row["LastName"].ToString()}",
                    IDCourse = Convert.ToInt32(row["IDCourse"].ToString()),
                    Content = row["Comment"].ToString(),
                    CreatedDate = DateTime.Parse(row["TimeDate"].ToString())
                };

                comments.Add(obj);
            }
            comments.Reverse();
            return comments;
        }
        public void Update()
        {
            _coureses.Clear();
            getCourse();
        }
    }
}
