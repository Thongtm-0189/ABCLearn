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
            return View();
        }
        public IActionResult About()
        {
            ViewBag.Lecturer = LecturerDAO.Instance().Lecturers();
            return View();
        }
        public IActionResult Course()
        {
            ViewBag.Course = CourseDAO.Instance().Courses();
            return View();
        }
        public IActionResult CourseDetail(int id)
        {

            Course course = CourseDAO.Instance().Courses().FirstOrDefault(x => x.Id == id);
            course.Comments = CourseDAO.Instance().getComment(course.Id);
            return View(@"Views/Home/DetailCourse.cshtml", course);
        }
    }
}