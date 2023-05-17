using ABCLearn.Models;
using System.Data;

namespace ABCLearn.DataContext
{
    public class LecturerDAO
    {
        private List<Lecturer> _lecturers = new List<Lecturer>();
        private static LecturerDAO insctance;
        public static LecturerDAO Instance()
        {
            if (insctance == null)
            {
                insctance = new LecturerDAO();
            }
            return insctance;
        }

        public List<Lecturer> Lecturers()
        {
            getLecturer();
            return _lecturers;
        }

        public void getLecturer()
        {
            if (_lecturers.Count == 0)
            {
                string query = "SELECT * FROM tblLecturer";
                DataTable dataTable = ConectionData.ExecuteQuery(query);
                foreach (DataRow row in dataTable.Rows)
                {
                    Lecturer obj = new Lecturer()
                    {
                        Id = Convert.ToInt32(row["IDLecturer"]),
                        FirstName = row["FirstName"].ToString(),
                        LastName = row["LastName"].ToString(),
                        RoleID = row["RoleID"].ToString(),
                        Email = row["Email"].ToString(),
                        Password = "8888888",
                        Phone = row["Phone"].ToString()
                        // Gán các giá trị khác của object từ các cột trong DataTable
                    };

                    _lecturers.Add(obj);
                }
            }
        }
        public List<Quiz> GetQuizzes(int id)
        {
            List<Quiz> quizzes = new List<Quiz>();
            string query = "SELECT * FROM tblLecturer";
            DataTable dataTable = ConectionData.ExecuteQuery(query);
            return quizzes;
        }
    }
}
