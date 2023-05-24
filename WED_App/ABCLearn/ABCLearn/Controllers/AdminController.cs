using ABCLearn.DataContext;
using ABCLearn.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ABCLearn.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View("Views/Admin/Index.cshtml");
        }
        public IActionResult Login(AccountLogin acc)
        {
            if (AdminDAO.Instence().Login(acc))
            {
                return RedirectToAction("pageStudent", "Admin", new { page = 0 });
            }
            return RedirectToAction("Index", "Admin");
        }
        public IActionResult Lecturer(int page = 0, int max = 1)
        {
            List<Lecturer> subList = new List<Lecturer>();
            if (page != (max - 1))
            {
                subList = LecturerDAO.Instance.Lecturers().GetRange(10 * page, 10);
            }
            else
            {
                subList = LecturerDAO.Instance.Lecturers().Skip(10 * page).ToList();
            }
            return View(subList);
        }
        public IActionResult Course(int page = 0, int max = 1)
        {
            List<Course> subList = new List<Course>();
            if (page != (max - 1))
            {
                subList = CourseDAO.Instance.Courses().GetRange(10 * page, 10);
            }
            else
            {
                subList = CourseDAO.Instance.Courses().Skip(10 * page).ToList();
            }
            return View(subList);
        }
        public IActionResult Calendar()
        {
            return View();
        }
        public IActionResult Nontification()
        {
            return View();
        }
        public IActionResult pageStudent(int page, int max)
        {
            List<Student> subList = new List<Student>();
            if (page != (max - 1))
            {
                subList = StudentDAO.Instance.Students().GetRange(10 * page, 10);
            }
            else
            {
                subList = StudentDAO.Instance.Students().Skip(10 * page).ToList();
            }
            return View(@"Views/Admin/AdminPage.cshtml", subList);
        }
        public IActionResult pageLecturer(int page, int max)
        {
            List<Student> subList = new List<Student>();
            if (page != (max - 1))
            {
                subList = StudentDAO.Instance.Students().GetRange(10 * page, 10);
            }
            else
            {
                subList = StudentDAO.Instance.Students().Skip(10 * page).ToList();
            }
            return View(@"Views/Admin/Lecturer.cshtml", subList);
        }
    }
}
