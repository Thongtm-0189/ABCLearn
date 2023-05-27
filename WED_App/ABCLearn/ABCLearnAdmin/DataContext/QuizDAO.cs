using ABCLearn.Models;
using System.Data;

namespace ABCLearn.DataContext
{
    public class QuizDAO
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
        public List<Quiz> quizzes()
        {
            if (_quizzes == null)
            {
                _quizzes = new List<Quiz>();
                _quizzes = GetQuiz();
            }
            return _quizzes;
        }
        public List<Quiz> GetQuiz()
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
                    IDLecturer = Convert.ToInt32(row["IDLecturer"].ToString().Trim()),
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
            return quizes;
        }

        public void update()
        {
            _quizzes.Clear();
            _quizzes = GetQuiz();
        }
    }
}
