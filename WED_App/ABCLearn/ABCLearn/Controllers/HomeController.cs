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
            LecturerDAO.Instance.Lecturers();
            StudentDAO.Instance.Students();
            CourseDAO.Instance.Courses();
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Course()
        {
            return View();
        }
        public IActionResult CourseDetail(int id)
        {
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
            return View();
        }
        public IActionResult Profile()
        {
            return View();
        }
        public IActionResult Commemt(int IDStudent, int IDCourse, string comment)
        {
            Course course = new Course();
            if (CourseDAO.Instance.Comment(IDStudent, IDCourse, comment))
            {
                course = CourseDAO.Instance.Courses().FirstOrDefault(x => x.Id == IDCourse);
                course.Comments = CourseDAO.Instance.getComment(course.Id);
            }
            return View(@"Views/Home/DetailCourse.cshtml", course);
        }
    }
}