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
        public IActionResult Quiz(int courseChoise = 0)
        {
            quizs.Clear();
            renderData();
            int numberQuiz = QuizDAO.Instance.quizzes().Count;
            string role = UserLogin.Instance.RoleID;
            if (role == "Student")
            {
                Random rand = new Random();
                HashSet<int> usedIndexes = new HashSet<int>(); // Sử dụng HashSet để theo dõi các chỉ số đã sử dụng

                for (int i = 0; i < 8; i++)
                {
                    int numberRandom;
                    if (courseChoise != 0)
                    {
                        numberQuiz = QuizDAO.Instance.quizzes().Where(x => x.IDCourse == courseChoise).ToList().Count;
                    }
                    do
                    {
                        numberRandom = rand.Next(0, numberQuiz);
                    }
                    while (usedIndexes.Contains(numberRandom)); // Lặp lại cho đến khi có chỉ số chưa được sử dụng
                    Quiz subQuiz = QuizDAO.Instance.quizzes()[numberRandom];
                    if (courseChoise != 0)
                    {
                        subQuiz = QuizDAO.Instance.quizzes().Where(x => x.IDCourse == courseChoise).ToList()[numberRandom];
                    }

                    usedIndexes.Add(numberRandom); // Đánh dấu chỉ số đã sử dụng

                    quizs.Add(subQuiz);
                }
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
        public IActionResult DetailQuiz(int IDCourse)
        {
            List<Quiz> quizzes = QuizDAO.Instance.quizzes().Where(x => x.IDCourse == IDCourse).ToList();
            return View(quizzes);
        }

        public IActionResult EditQuiz(string btnQuiz, Quiz quiz)
        {
            if (btnQuiz == "Edit")
            {
                if (QuizDAO.Instance.UpdateQuiz(quiz))
                {
                    QuizDAO.Instance.update();
                    RedirectToAction("DetailQuiz", "Quiz", new { IDCourse = quiz.IDCourse });
                }
            }
            else
            {
                if (QuizDAO.Instance.DeleteQuiz(quiz))
                {
                    QuizDAO.Instance.update();
                    RedirectToAction("DetailQuiz", "Quiz", new { IDCourse = quiz.IDCourse });
                }
            }
            return RedirectToAction("DetailQuiz", "Quiz", new { IDCourse = quiz.IDCourse });
        }
        public IActionResult Addnew(Quiz quiz)
        {
            QuizDAO.Instance.Addnew(quiz);
            QuizDAO.Instance.update();
            return RedirectToAction("DetailQuiz", "Quiz", new { IDCourse = quiz.IDCourse });
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
