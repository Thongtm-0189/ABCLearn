using ABCLearn.Models;
using System.Data;

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
                        IDLecturer = Convert.ToInt32(row["IDLecturer"]),
                        Title = row["Title"].ToString(),
                        Detail = row["Detail"].ToString(),
                        Price = float.Parse(row["Price"].ToString()),
                        Sale = float.Parse(row["Sale"].ToString())
                    };

                    _coureses.Add(obj);
                }
            }
        }
    }
}
