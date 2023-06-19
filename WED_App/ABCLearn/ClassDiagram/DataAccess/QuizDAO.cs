using ABCLearn.DataAccess;
using ABCLearn.Models;
using System.Data;

namespace ABCLearn.DataContext
{
    public class QuizDAO : IServiceDAO<Quiz, QuizDAO>
    {
        private List<Quiz> _quizzes;
        private static QuizDAO instance;
        private QuizDAO() { }
        public static QuizDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new QuizDAO();
                }
                return instance;
            }
        }
        public List<Quiz> Quizzes()
        {
            if (_quizzes == null)
            {
                _quizzes = new List<Quiz>();
                GetAll();
            }
            return _quizzes;
        }
        public void GetAll()
        {
            List<Quiz> quizes = new List<Quiz>();
            string query = "SELECT * FROM tblQuiz";
            DataTable dataTable = ConectionData.ExecuteQuery(query);
            dataTable = ConectionData.ExecuteQuery(query);
            foreach (DataRow row in dataTable.Rows)
            {
                Quiz obj = new Quiz()
                {
                    Id = Convert.ToInt32(row["IDQuiz"].ToString().Trim()),
                    IDCourse = Convert.ToInt32(row["IDCourse"].ToString().Trim()),
                    Question = row["Question"].ToString().Trim(),
                    AnswerA = row["AnswerA"].ToString().Trim(),
                    AnswerB = row["AnswerB"].ToString().Trim(),
                    AnswerC = row["AnswerC"].ToString().Trim(),
                    AnswerD = row["AnswerD"].ToString().Trim(),
                    CorrectAnswer = row["CorrectAnswer"].ToString().Trim()
                    // Gán các giá trị khác của object từ các cột trong DataTable
                };
                quizes.Add(obj);
            }
        }
        public bool Update(Quiz quiz)
        {
            string query = "UPDATE tblQuiz " +
                " \nSET Question = @question , AnswerA = @A , AnswerB = @B , AnswerC = @C , AnswerD = @D , CorrectAnswer = @CA " +
                " \nWHERE IDQuiz = @IDQuiz";
            bool result = ConectionData.ExecuteUpdate(query, new object[] { quiz.Question, quiz.AnswerA, quiz.AnswerB, quiz.AnswerC, quiz.AnswerD, quiz.CorrectAnswer, quiz.Id });
            return result;
        }
        public bool Delete(int id)
        {
            string query = "DELETE FROM tblQuiz WHERE IDQuiz = @idStudent ";
            bool result = ConectionData.ExecuteUpdate(query, new object[] { id });
            return result;
        }
        public bool DeleteAll(int Idcourse)
        {
            string query = "DELETE FROM tblQuiz WHERE IDCourse = @id ";
            bool result = ConectionData.ExecuteUpdate(query, new object[] { Idcourse });
            return result;
        }
        public bool AddNew(Quiz quiz)
        {
            int ID = UserLogin.Instance.Id;
            string query = "INSERT INTO tblQuiz " +
                "\nVALUES ( @question , @A , @B , @C , @D , @CA , @Idcourse )";
            bool result = ConectionData.ExecuteUpdate(query, new object[] { quiz.Question, quiz.AnswerA, quiz.AnswerB, quiz.AnswerC, quiz.AnswerD, quiz.CorrectAnswer, quiz.IDCourse });
            return result;
        }
        public void SaveChange()
        {
            _quizzes.Clear();
            GetAll();
        }
    }
}
