using ABCLearn.DataContext;
using ABCLearn.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ABCLearn.Controllers
{
    public class QuizController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        private List<Quiz> quizs = new List<Quiz>();
        public IActionResult Quiz()
        {
            quizs.Clear();
            renderData();
            int numberQuiz = QuizDAO.Instance.quizzes().Count;
            string role = UserLogin.Instance.RoleID;
            if (role == "Student")
            {
                Random rand = new Random();
                HashSet<int> usedIndexes = new HashSet<int>(); // Sử dụng HashSet để theo dõi các chỉ số đã sử dụng

                for (int i = 0; i < 10; i++)
                {
                    int numberRandom;
                    do
                    {
                        numberRandom = rand.Next(0, numberQuiz);
                    }
                    while (usedIndexes.Contains(numberRandom)); // Lặp lại cho đến khi có chỉ số chưa được sử dụng

                    usedIndexes.Add(numberRandom); // Đánh dấu chỉ số đã sử dụng

                    Quiz subQuiz = QuizDAO.Instance.quizzes()[numberRandom];
                    quizs.Add(subQuiz);
                }
            }
            else if (role == "Lecturer")
            {
                int idLecturer = UserLogin.Instance.Id;
                quizs = QuizDAO.Instance.quizzes().Where(q => q.IDLecturer == idLecturer).ToList();
            }

            if (!UserLogin.Instance.Islogin)
            {
                quizs = QuizDAO.Instance.quizzes().Take(5).ToList();
            }

            return View(quizs);
        }
        public IActionResult SubmitQuiz(Dictionary<int, string> answer)
        {
            var score = CalculatorScore(answer);
            ViewBag.Score = score;
            return View(answer);
        }
        private int CalculatorScore(Dictionary<int, string> answers)
        {
            int score = 0;
            foreach (KeyValuePair<int, string> entry in answers)
            {
                int idQuiz = entry.Key;
                string answer = entry.Value;
                bool isCorrect = QuizDAO.Instance.quizzes().Find(q => q.Id == idQuiz).CorrectAnswer == answer;
                if (isCorrect)
                {
                    score++;
                }
            }
            return score;
        }
        private void renderData()
        {
            LecturerDAO.Instance.Lecturers();
            StudentDAO.Instance.Students();
            CourseDAO.Instance.Courses();
            QuizDAO.Instance.quizzes();
        }
    }
}
