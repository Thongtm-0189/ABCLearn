using ABCLearn.DataContext;
using ABCLearn.Extend;
using ABCLearn.Models;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.IO;
using System.Collections.Generic;

namespace ABCLearn.Controllers
{
    public class QuizController : Controller
    {
        private readonly IWebHostEnvironment _environment;

        public QuizController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        public IActionResult Index()
        {
            SessionUser();
            return View();
        }
        private List<Quiz> quizs = new List<Quiz>();
        public IActionResult Quiz(int courseChoise = 0)
        {
            if (HttpContext.Session.GetObject<UserLogin>("User") == null)
            {
                UserLogin.Instance = null;
            }
            quizs.Clear();
            int numberQuiz = QuizDAO.Instance.Quizzes().Count;

            if (HttpContext.Session.GetObject<UserLogin>("User") == null)
            {
                quizs = QuizDAO.Instance.Quizzes().Take(5).ToList();
            }
            else
            {
                Random rand = new Random();
                HashSet<int> usedIndexes = new HashSet<int>(); // Sử dụng HashSet để theo dõi các chỉ số đã sử dụng
                if (courseChoise != 0)
                {
                    numberQuiz = QuizDAO.Instance.Quizzes().Where(x => x.IDCourse == courseChoise).ToList().Count;
                    for (int i = 0; i < numberQuiz; i++)
                    {
                        int numberRandom;
                        do
                        {
                            numberRandom = rand.Next(0, numberQuiz);
                        }
                        while (usedIndexes.Contains(numberRandom)); // Lặp lại cho đến khi có chỉ số chưa được sử dụng
                        Quiz subQuiz = QuizDAO.Instance.Quizzes()[numberRandom];

                        subQuiz = QuizDAO.Instance.Quizzes().Where(x => x.IDCourse == courseChoise).ToList()[numberRandom];

                        usedIndexes.Add(numberRandom); // Đánh dấu chỉ số đã sử dụng

                        quizs.Add(subQuiz);
                    }
                }
                else
                {
                    quizs = QuizDAO.Instance.Quizzes().Take(2).ToList();
                }
            }

            SessionUser();
            return View(quizs);
        }
        public IActionResult SubmitQuiz(Dictionary<int, string> answer)
        {
            if (HttpContext.Session.GetObject<UserLogin>("User") == null)
            {
                UserLogin.Instance = null;
            }
            renderData();
            var score = CalculatorScore(answer);
            ViewBag.Score = score;
            SessionUser();
            return View(answer);
        }
        private int CalculatorScore(Dictionary<int, string> answers)
        {
            if (HttpContext.Session.GetObject<UserLogin>("User") == null)
            {
                UserLogin.Instance = null;
            }
            int score = 0;
            foreach (KeyValuePair<int, string> entry in answers)
            {
                int idQuiz = entry.Key;
                string answer = entry.Value;
                bool isCorrect = QuizDAO.Instance.Quizzes().Find(q => q.Id == idQuiz).CorrectAnswer == answer;
                if (isCorrect)
                {
                    score++;
                }
            }
            SessionUser();
            return score;
        }
        public IActionResult DetailQuiz(int IDCourse)
        {
            if (HttpContext.Session.GetObject<UserLogin>("User") == null)
            {
                UserLogin.Instance = null;
            }
            List<Quiz> quizzes = new List<Quiz>();
            quizzes = QuizDAO.Instance.Quizzes().Where(x => x.IDCourse == IDCourse).ToList();
            SessionUser();
            ViewBag.CourseTitle = CourseDAO.Instance.Courses().Find(x => x.Id == IDCourse).Title;
            ViewBag.Idcourse = IDCourse;
            return View(quizzes);
        }

        public IActionResult EditQuiz(string btnQuiz, Quiz quiz)
        {
            if (HttpContext.Session.GetObject<UserLogin>("User") == null)
            {
                UserLogin.Instance = null;
            }
            if (btnQuiz == "Edit")
            {
                if (QuizDAO.Instance.Update(quiz))
                {
                    QuizDAO.Instance.SaveChange();
                    RedirectToAction("DetailQuiz", "Quiz", new { IDCourse = quiz.IDCourse });
                }
            }
            else
            {
                if (QuizDAO.Instance.Delete(quiz.Id))
                {
                    QuizDAO.Instance.SaveChange();
                    RedirectToAction("DetailQuiz", "Quiz", new { IDCourse = quiz.IDCourse });
                }
            }
            SessionUser();
            return RedirectToAction("DetailQuiz", "Quiz", new { IDCourse = quiz.IDCourse });
        }
        public IActionResult Addnew(Quiz quiz)
        {
            if (HttpContext.Session.GetObject<UserLogin>("User") == null)
            {
                UserLogin.Instance = null;
            }
            QuizDAO.Instance.AddNew(quiz);
            QuizDAO.Instance.SaveChange();
            CourseDAO.Instance.SaveChange();
            SessionUser();
            return RedirectToAction("DetailQuiz", "Quiz", new { IDCourse = quiz.IDCourse });
        }
        private void renderData()
        {
            LecturerDAO.Instance.GetAll();
            StudentDAO.Instance.GetAll();
            CourseDAO.Instance.GetAll();
            QuizDAO.Instance.GetAll();
        }
        private void SessionUser()
        {
            var user = HttpContext.Session.GetObject<UserLogin>("User");
            if (user != null)
            {
                ViewBag.Role = user.RoleID;
                ViewBag.login = true;
                ViewBag.user = user;
            }
            else
            {
                ViewBag.Role = "Guest";
                ViewBag.login = false;
            }
        }
        public IActionResult importFileQuiz(IFormFile fileExcel, int CourseID)
        {
            if (fileExcel != null && fileExcel.Length > 0 && (Path.GetExtension(fileExcel.FileName) == ".xls" || Path.GetExtension(fileExcel.FileName) == ".xlsx"))
            {
                var fileName = $"FileQuiz{Path.GetExtension(fileExcel.FileName)}";

                var path = Path.Combine(_environment.WebRootPath, $"File", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    fileExcel.CopyTo(fileStream);
                }
                readInsertQuiz(CourseID);
                QuizDAO.Instance.SaveChange();
            }
            SessionUser();
            return RedirectToAction("DetailQuiz", "Quiz", new { IDCourse = CourseID });
        }
        private void readInsertQuiz(int Idcourse)
        {
            string filePath = "wwwroot/File/FileQuiz.xlsx"; // Đường dẫn tới file Excel trên server
            FileInfo fileInfo = new FileInfo(filePath);
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage(fileInfo))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0]; // Lấy sheet đầu tiên

                int startRow = worksheet.Dimension.Start.Row + 1; // Dòng bắt đầu chứa dữ liệu
                int endRow = worksheet.Dimension.End.Row; // Dòng kết thúc chứa dữ liệu

                for (int row = startRow; row <= endRow; row++)
                {
                    try
                    {
                        int column = 1;
                        string question = worksheet.Cells[row, column++].Value.ToString();
                        string answerA = worksheet.Cells[row, column++].Value.ToString();
                        string answerB = worksheet.Cells[row, column++].Value.ToString();
                        string answerC = worksheet.Cells[row, column++].Value.ToString();
                        string answerD = worksheet.Cells[row, column++].Value.ToString();
                        string answerCR = worksheet.Cells[row, column++].Value.ToString();
                        Quiz quiz = new Quiz() { Question = question, AnswerA = answerA, AnswerB = answerB, AnswerC = answerC, AnswerD = answerD, CorrectAnswer = answerCR, IDCourse = Idcourse };
                        QuizDAO.Instance.AddNew(quiz);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Invalid data at row " + row + ": " + ex.Message);
                    }
                }
            }
        }
        public IActionResult removeAllQuiz(int CourseID)
        {
            QuizDAO.Instance.DeleteAll(CourseID);
            QuizDAO.Instance.SaveChange();
            return RedirectToAction("DetailQuiz", "Quiz", new { IDCourse = CourseID });
        }
    }
}
