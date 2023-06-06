using ABCLearn.DataContext;
using ABCLearn.Models;
using Microsoft.AspNetCore.Mvc;

namespace ABCLearn.Controllers
{
    public class CourseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ViewCourse(int IdCourse)
        {
            if (!UserLogin.Instance.Islogin || UserLogin.Instance.RoleID != "Admin")
            {
                return RedirectToAction("Index", "Admin");
            }
            Course course = CourseDAO.Instance.Courses().Find(x => x.Id == IdCourse);
            return View(course);
        }
        public IActionResult AddNewCourse(Course course)
        {
            if (!UserLogin.Instance.Islogin || UserLogin.Instance.RoleID != "Admin")
            {
                return RedirectToAction("Index", "Admin");
            }
            var isInsert = CourseDAO.Instance.AddNewCourse(course);
            if (isInsert)
            {
                CourseDAO.Instance.Update();
                LecturerDAO.Instance.Update();
            }
            return RedirectToAction("Course", "Admin");
        }
        public IActionResult UpdateCourse(Course course)
        {
            if (!UserLogin.Instance.Islogin || UserLogin.Instance.RoleID != "Admin")
            {
                return RedirectToAction("Index", "Admin");
            }
            var isInsert = CourseDAO.Instance.UpdateCourse(course);
            if (isInsert)
            {
                CourseDAO.Instance.Update();
                LecturerDAO.Instance.Update();
            }
            return RedirectToAction("ViewCourse", "Course", new { IdCourse = course.Id });
        }
        public IActionResult SetStatus(Course course)
        {
            if (!UserLogin.Instance.Islogin || UserLogin.Instance.RoleID != "Admin")
            {
                return RedirectToAction("Index", "Admin");
            }
            var isInsert = CourseDAO.Instance.setStatus(course);
            if (isInsert)
            {
                CourseDAO.Instance.Update();
                LecturerDAO.Instance.Update();
            }
            return RedirectToAction("Course", "Admin");
        }
        private void renderData()
        {
            LecturerDAO.Instance.getLecturer();
            StudentDAO.Instance.getStudents();
            CourseDAO.Instance.getCourse();
            QuizDAO.Instance.GetQuiz();
        }
    }
}
