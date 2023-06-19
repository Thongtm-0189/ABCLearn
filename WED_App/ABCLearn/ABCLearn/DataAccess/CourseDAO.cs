using ABCLearn.DataAccess;
using ABCLearn.Models;
using ABCLearn.Services;
using NuGet.Packaging.Signing;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using Calendar = ABCLearn.Models.Calendar;

namespace ABCLearn.DataContext
{
    public class CourseDAO : IServiceDAO<Course, CourseDAO>
    {
        private List<Course> _coureses = new List<Course>();
        private static CourseDAO insctance;
        public static CourseDAO Instance
        {
            get
            {
                if (insctance == null)
                {
                    insctance = new CourseDAO();
                }
                return insctance;
            }
        }

        public List<Course> Courses()
        {
            if (_coureses.Count == 0)
            {
                GetAll();
            }
            return _coureses;
        }
        public void GetAll()
        {
            _coureses.Clear();
            if (_coureses.Count == 0)
            {
                string query = "SELECT * FROM tblCourse";
                DataTable dataTable = ConectionData.ExecuteQuery(query);
                foreach (DataRow row in dataTable.Rows)
                {

                    Course obj = new Course()
                    {
                        Id = Convert.ToInt32(row["IDCourse"].ToString().Trim()),
                        Title = row["Title"].ToString().Trim(),
                        Detail = row["Detail"].ToString().Trim(),
                        Status = bool.Parse(row["Status"].ToString().Trim()),
                        Calendars = getCalendar(Convert.ToInt32(row["IDCourse"].ToString().Trim())),
                        Price = float.Parse(row["Price"].ToString().Trim()),
                        Students = StudentCourse(Convert.ToInt32(row["IDCourse"].ToString().Trim())),
                        Sale = float.Parse(row["Sale"].ToString().Trim()),
                        Quantity = int.Parse(row["Quantity"].ToString().Trim())
                    };
                    if (row["IDLecturer"].ToString().Trim() != "")
                    {
                        obj.Lecturer = LecturerDAO.Instance.Lecturers().FirstOrDefault(x => x.Id == Convert.ToInt32(row["IDLecturer"].ToString().Trim()));
                    }

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
                    StudentName = $"{row["FirstName"].ToString().Trim()} {row["LastName"].ToString().Trim()}",
                    IDCourse = Convert.ToInt32(row["IDCourse"].ToString().Trim()),
                    Content = row["Comment"].ToString().Trim(),
                    CreatedDate = DateTime.Parse(row["TimeDate"].ToString().Trim())
                };

                comments.Add(obj);
            }
            comments.Reverse();
            comments.Reverse();
            if (comments.Count > 10)
            {
                return comments.GetRange(0, 10);
            }
            return comments;
        }
        public List<Student> StudentCourse(int id)
        {
            List<Student> StudentCourse = new List<Student>();
            string query = "SELECT * FROM tblCourseOfStudent, tblStudent WHERE tblStudent.IDStudent = tblCourseOfStudent.IDStudent AND IDCourse = @id";
            DataTable dataTable = ConectionData.ExecuteQuery(query, new object[] { id });
            foreach (DataRow row in dataTable.Rows)
            {
                Student obj = new Student()
                {
                    Id = Convert.ToInt32(row["IDStudent"].ToString().Trim()),
                    FirstName = row["FirstName"].ToString().Trim(),
                    LastName = row["LastName"].ToString().Trim(),
                    RoleID = row["RoleID"].ToString().Trim(),
                    Password = row["Password"].ToString().Trim(),
                    Email = row["Email"].ToString().Trim(),
                    Phone = row["Phone"].ToString().Trim(),
                    Avatar = row["Avatar"].ToString().Trim(),
                    Gander = row["Gander"].ToString().Trim(),
                    DOB = DateTime.Parse(row["DOB"].ToString().Trim()),
                    IsConfirmEmail = Boolean.Parse(row["ConfirmEmail"].ToString().Trim()),
                    DateCreated = DateTime.Parse(row["DateCreate"].ToString().Trim())
                };
                StudentCourse.Add(obj);
            }
            return StudentCourse;
        }
        public List<Calendar> getCalendar(int id)
        {
            List<Calendar> calendar = new List<Calendar>();
            string query = "SELECT * FROM tblCalendar WHERE IDCourse = @id";
            DataTable dataTable = ConectionData.ExecuteQuery(query, new object[] { id });
            foreach (DataRow row in dataTable.Rows)
            {
                Calendar obj = new Calendar()
                {
                    Id = Convert.ToInt32(row["IDCalendar"].ToString().Trim()),
                    IDCourse = Convert.ToInt32(row["IDCourse"].ToString().Trim()),
                    StartTime = TimeSpan.Parse(row["TimeStart"].ToString().Trim()),
                    EndTime = TimeSpan.Parse(row["TimeEnd"].ToString().Trim())
                };

                calendar.Add(obj);
            }
            return calendar;
        }

        public bool Comment(int IDStudent, int IDCourse, string comment)
        {
            DateTime vietnamTime = CurrentDateTime.GetcurrentDateTime;
            string query = "INSERT INTO tblCommentOfStudent VALUES ( @IDStudent , @IDCourse , @Comment , @TimeDate ) ";
            return ConectionData.ExecuteUpdate(query, new object[] { IDStudent, IDCourse, comment, vietnamTime.ToString("yyyy-MM-dd HH:mm:ss") });
        }
        public bool AddNew(Course course)
        {
            bool result = false;
            string query = "INSERT INTO tblCourse VALUES " +
                "( @idLecturer , @title , @detail , @price , @sale , 'True', 0) ";
            result = ConectionData.ExecuteUpdate(query, new object[] { course.IDLecturer, course.Title, course.Detail, course.Price, course.Sale });
            return result;
        }
        public bool Update(Course course)
        {
            bool result = false;
            string query = "UPDATE tblCourse SET " +
                " IDLEcturer = @idLecturer , Title = @title , Detail = @detail , Price = @price , Sale = @sale WHERE IDCourse = @id";
            result = ConectionData.ExecuteUpdate(query, new object[] { course.IDLecturer, course.Title, course.Detail, course.Price, course.Sale, course.Id });
            return result;
        }
        public bool setStatus(Course course)
        {
            bool result = false;
            string query = "UPDATE tblCourse SET " +
                " Status = 1 WHERE IDCourse = @id ";
            if (course.Status)
            {
                query = "UPDATE tblCourse SET " +
                " Status = 0 WHERE IDCourse = @id ";
            }
            result = ConectionData.ExecuteUpdate(query, new object[] { course.Id });
            return result;
        }
        public bool BuyCourse(int idCourse, int idStudent)
        {
            bool result = false;
            string query = "INSERT INTO tblCourseOfStudent VALUES " +
                " ( @IDstudetn , @Course , @date ) ";
            DateTime vietnamTime = CurrentDateTime.GetcurrentDateTime;
            result = ConectionData.ExecuteUpdate(query, new object[] { idStudent, idCourse, vietnamTime.ToString("yyyy-MM-dd HH:mm:ss") });
            return result;
        }
        public void SaveChange()
        {
            _coureses.Clear();
            GetAll();
        }

        public bool Delete(int obj)
        {
            throw new NotImplementedException();
        }
    }
}
