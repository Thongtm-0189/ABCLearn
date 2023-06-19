using ABCLearn.DataAccess;
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
            //renderData();
            SessionUser();
            return View();
        }
        public IActionResult About()
        {
            SessionUser();
            return View();
        }
        public IActionResult Course()
        {
            SessionUser();
            return View();
        }
        public IActionResult CourseDetail(int id)
        {
            SessionUser();
            Course course = CourseDAO.Instance.Courses().FirstOrDefault(x => x.Id == id);
            course.Comments = CourseDAO.Instance.getComment(course.Id);
            return View(@"Views/Home/DetailCourse.cshtml", course);
        }
        public IActionResult Contact()
        {
            SessionUser();
            return View();
        }
        public IActionResult CheckGarmarly()
        {
            SessionUser();
            return View();
        }
        public IActionResult Calendar()
        {
            SessionUser();
            return View();
        }
        public IActionResult Profile()
        {
            SessionUser();
            var user = HttpContext.Session.GetObject<UserLogin>("User");
            if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }
            List<Transaction> trans = new List<Transaction>();

            if (user.RoleID == "Student")
            {
                trans = TransactionDAO.Instanse.Transactions().Where(x => x.IdStudent == user.Id).ToList();
            }
            return View(trans);
        }
        public IActionResult Commemt(int IDStudent, int IDCourse, string comment)
        {
            SessionUser();
            Course course = new Course();
            if (CourseDAO.Instance.Comment(IDStudent, IDCourse, comment))
            {
                course = CourseDAO.Instance.Courses().FirstOrDefault(x => x.Id == IDCourse);
                course.Comments = CourseDAO.Instance.getComment(course.Id);
                CourseDAO.Instance.SaveChange();
                LecturerDAO.Instance.SaveChange();
            }
            return View(@"Views/Home/DetailCourse.cshtml", course);
        }

        public IActionResult ViewLecturer(int IDLecturer)
        {
            SessionUser();
            Lecturer lecturer = LecturerDAO.Instance.Lecturers().Find(x => x.Id == IDLecturer);
            lecturer.Courses.ForEach(x => x.Calendars = CourseDAO.Instance.getCalendar(x.Id));
            return View(lecturer);
        }
        public IActionResult StudentCourse(int IDCourse)
        {
            if (HttpContext.Session.GetObject<UserLogin>("User") == null)
            {
                return RedirectToAction("Index", "Login");
            }
            SessionUser();
            List<Student> list = CourseDAO.Instance.StudentCourse(IDCourse);
            ViewBag.IdCourse = IDCourse;
            return View(list);
        }
        public IActionResult searchStudentByLastname(string LastName, int IDCourse)
        {
            SessionUser();
            List<Student> list = CourseDAO.Instance.StudentCourse(IDCourse).Where(x => x.LastName.Contains(LastName)).ToList();
            return View("Views/Home/StudentCourse.cshtml", list);
        }
        private void renderData()
        {
            LecturerDAO.Instance.GetAll();
            StudentDAO.Instance.GetAll();
            CourseDAO.Instance.GetAll();
            QuizDAO.Instance.GetAll();
            TransactionDAO.Instanse.Transactions();
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
        }
    }
}