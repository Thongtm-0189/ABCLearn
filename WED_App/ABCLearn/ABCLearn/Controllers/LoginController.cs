using ABCLearn.DataContext;
using ABCLearn.Models;
using Microsoft.AspNetCore.Mvc;

namespace ABCLearn.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login(AccountLogin acc, string submitButton)
        {
            ViewData["check"] = submitButton;
            if (submitButton == "Sutdent")
            {
                if (StudentDAO.Instance.login(acc))
                {
                    UserLogin.Instance.Islogin = true;
                    UserLogin.Instance.RoleID = "Student";
                    UserLogin.Instance.TimeLogin = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                    foreach (var course in UserLogin.Instance.Courses)
                    {
                        course.Calendars = CourseDAO.Instance.getCalendar(course.Id);
                    }
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["MessError"] = "Pass Or Email was Wrong!!";
                    return RedirectToAction("Index", "Login");
                }
            }
            else
            {
                if (LecturerDAO.Instance.login(acc))
                {
                    UserLogin.Instance.Islogin = true;
                    UserLogin.Instance.RoleID = "Lecturer";
                    UserLogin.Instance.TimeLogin = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                    foreach (var course in UserLogin.Instance.Courses)
                    {
                        course.Calendars = CourseDAO.Instance.getCalendar(course.Id);
                    }
                    return RedirectToAction("Profile", "Home");
                }
                else
                {
                    TempData["MessError"] = "Pass Or Email was Wrong!!";
                    return RedirectToAction("Index", "Login");
                }
            }
        }
        public IActionResult Logout()
        {
            UserLogin.Instance.Islogin = false;
            return RedirectToAction("Index", "Home");
        }
    }
}
