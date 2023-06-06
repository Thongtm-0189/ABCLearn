using ABCLearn.DataContext;
using ABCLearn.Extend;
using ABCLearn.Models;
using ABCLearn.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace ABCLearn.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult forgotPass()
        {
            if (HttpContext.Session.GetObject<UserLogin>("User") == null)
            {
                UserLogin.Instance = null;
            }
            return View();
        }
        public IActionResult Login(AccountLogin acc, string submitButton)
        {
            if (submitButton == "Sutdent")
            {
                if (StudentDAO.Instance.login(acc))
                {
                    UserLogin.Instance.Islogin = true;
                    UserLogin.Instance.RoleID = "Student";
                    foreach (var course in UserLogin.Instance.Courses)
                    {
                        course.Calendars = CourseDAO.Instance.getCalendar(course.Id);
                    }
                    HttpContext.Session.SetObject("User", UserLogin.Instance);
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
                    HttpContext.Session.SetObject("User", UserLogin.Instance);
                    return RedirectToAction("Profile", "Home");
                }
                else
                {
                    TempData["MessError"] = "Pass Or Email was Wrong!!";
                    return RedirectToAction("Index", "Login");
                }
            }
        }
        public IActionResult ConfirmEmail(Profile pro, string forgot)
        {
            if (pro != null)
            {
                Profile.Instance = pro;
                string email = pro.Email;
                (bool, int) afterSend = Email.Instance.sendOTP(email, forgot);
                bool isSend = afterSend.Item1;
                int OTP = afterSend.Item2;
                if (isSend)
                {
                    ViewBag.Email = email;
                    ViewBag.OTP = OTP;
                    return View("Views/Login/ConfirmEmail.cshtml", forgot);
                }
            }
            TempData["MessError"] = "ERROR in register form let try again!!";
            return RedirectToAction("Index", "Login");
        }
        public IActionResult AcceptanceEmail(string forgot)
        {
            if (forgot == "reset")
            {
                return RedirectToAction("forgotPass", "Login");
            }
            else
            {
                if (StudentDAO.Instance.insertStudent(Profile.Instance))
                {
                    Profile.Instance = null;
                }
            }
            TempData["MessSS"] = "Success!!";
            StudentDAO.Instance.upDate();
            return RedirectToAction("Index", "Login");
        }
        public IActionResult Logout()
        {
            UserLogin.Instance.Islogin = false;
            UserLogin.Instance.RoleID = "Guest";
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        private void renderData()
        {
            LecturerDAO.Instance.getLecturer();
            StudentDAO.Instance.getStudents();
            CourseDAO.Instance.getCourse();
            QuizDAO.Instance.GetQuiz();
        }
        public IActionResult ResetPassword(string Password)
        {
            var user = HttpContext.Session.GetObject<UserLogin>("User");
            if (user.RoleID == "Student")
            {
                Profile.Instance.Password = Password;
                if (StudentDAO.Instance.resetPassword(Profile.Instance))
                {
                    Profile.Instance = null;
                    TempData["MessSS"] = "Success!!";
                    StudentDAO.Instance.upDate();
                    return RedirectToAction("Index", "Login");
                }
            }
            else
            {
                Profile.Instance.Password = Password;
                if (LecturerDAO.Instance.resetPassword(Profile.Instance))
                {
                    Profile.Instance = null;
                    TempData["MessSS"] = "Success!!";
                    LecturerDAO.Instance.Update();
                    return RedirectToAction("Index", "Login");
                }
            }
            TempData["MessError"] = "ERROR in reset password!!";
            return RedirectToAction("Index", "Login");
        }
    }
}
