using ABCLearn.DataContext;
using ABCLearn.Extend;
using ABCLearn.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Diagnostics;

namespace ABCLearn.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            renderData();
            return View();
        }

        public IActionResult Login(AccountLogin acc)
        {
            renderData();
            if (AdminDAO.Instence().Login(acc))
            {
                UserLogin.Instance.Email = acc.Email;
                UserLogin.Instance.Password = acc.Password;
                UserLogin.Instance.RoleID = "Admin";
                UserLogin.Instance.Islogin = true;
                HttpContext.Session.SetObject("ADMIN", UserLogin.Instance);
                return RedirectToAction("Student", "Admin", new { page = 0 });
            }
            return RedirectToAction("Index", "Admin");
        }
        public IActionResult Lecturer(int page = 0, int max = 1)
        {
            renderData();
            if (HttpContext.Session.GetObject<UserLogin>("ADMIN") == null)
            {
                return RedirectToAction("Index", "Admin");
            }

            List<Lecturer> subList = new List<Lecturer>();
            if (page != (max - 1))
            {
                subList = LecturerDAO.Instance.Lecturers().GetRange(8 * page, 8);
            }
            else
            {
                subList = LecturerDAO.Instance.Lecturers().Skip(8 * page).ToList();
            }
            return View(subList);
        }
        public IActionResult Course(int page = 0, int max = 1)
        {
            renderData();
            if (HttpContext.Session.GetObject<UserLogin>("ADMIN") == null)
            {
                return RedirectToAction("Index", "Admin");
            }

            List<Course> subList = new List<Course>();
            if (page != (max - 1))
            {
                subList = CourseDAO.Instance.Courses().GetRange(8 * page, 8);
            }
            else
            {
                subList = CourseDAO.Instance.Courses().Skip(8 * page).ToList();
            }
            return View(subList);
        }
        public IActionResult Calendar()
        {
            renderData();
            if (HttpContext.Session.GetObject<UserLogin>("ADMIN") == null)
            {
                return RedirectToAction("Index", "Admin");
            }

            return View();
        }
        public IActionResult Nontification()
        {
            if (HttpContext.Session.GetObject<UserLogin>("ADMIN") == null)
            {
                return RedirectToAction("Index", "Admin");
            }

            return View();
        }
        public IActionResult Student(int page = 0, int max = 0)
        {
            renderData();
            if (HttpContext.Session.GetObject<UserLogin>("ADMIN") == null)
            {
                return RedirectToAction("Index", "Admin");
            }

            List<Student> subList = new List<Student>();
            if (page != (max - 1))
            {
                subList = StudentDAO.Instance.Students().GetRange(8 * page, 8);
            }
            else
            {
                subList = StudentDAO.Instance.Students().Skip(8 * page).ToList();
            }
            return View(@"Views/Admin/AdminPage.cshtml", subList);
        }
        public IActionResult LogOut()
        {
            UserLogin.Instance.Email = "";
            UserLogin.Instance.Password = "";
            UserLogin.Instance.RoleID = "";
            UserLogin.Instance.Islogin = false;
            return RedirectToAction("Index", "Admin");
        }

        private void renderData()
        {
            LecturerDAO.Instance.GetAll();
            StudentDAO.Instance.GetAll();
            CourseDAO.Instance.GetAll();
            QuizDAO.Instance.GetAll();
        }
    }
}
