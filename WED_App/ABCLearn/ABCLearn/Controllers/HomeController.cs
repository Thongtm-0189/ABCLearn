using ABCLearn.DataContext;
using ABCLearn.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ABCLearn.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Lecturer = LecturerDAO.Instance().Lecturers();
            ViewBag.Course = CourseDAO.Instance().Courses();

            return View();
        }
        public IActionResult About()
        {
            ViewBag.Lecturer = LecturerDAO.Instance().Lecturers();
            return View();
        }
    }
}