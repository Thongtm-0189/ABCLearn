using ABCLearn.DataContext;
using ABCLearn.Extend;
using ABCLearn.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ABCLearn.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            renderData();
            return View();
        }
        public IActionResult About()
        {
            renderData();
            return View();
        }
        public IActionResult Course()
        {
            renderData();
            return View();
        }
        public IActionResult CourseDetail(int id)
        {
            renderData();
            Course course = CourseDAO.Instance.Courses().FirstOrDefault(x => x.Id == id);
            course.Comments = CourseDAO.Instance.getComment(course.Id);
            return View(@"Views/Home/DetailCourse.cshtml", course);
        }
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult CheckGarmarly()
        {
            return View();
        }
        public IActionResult Calendar()
        {
            renderData();
            return View();
        }
        public IActionResult Profile()
        {
            renderData();
            if (!UserLogin.Instance.Islogin)
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }
        public IActionResult Commemt(int IDStudent, int IDCourse, string comment)
        {
            renderData();
            Course course = new Course();
            if (CourseDAO.Instance.Comment(IDStudent, IDCourse, comment))
            {
                course = CourseDAO.Instance.Courses().FirstOrDefault(x => x.Id == IDCourse);
                course.Comments = CourseDAO.Instance.getComment(course.Id);
            }
            return View(@"Views/Home/DetailCourse.cshtml", course);
        }

        public IActionResult ViewLecturer(int IDLecturer)
        {
            renderData();
            Lecturer lecturer = LecturerDAO.Instance.Lecturers().Find(x => x.Id == IDLecturer);
            lecturer.Courses.ForEach(x => x.Calendars = CourseDAO.Instance.getCalendar(x.Id));
            return View(lecturer);
        }
        public IActionResult StudentCourse(int IDCourse)
        {
            List<Student> list = CourseDAO.Instance.StudentCourse(IDCourse);

            return View(list);
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