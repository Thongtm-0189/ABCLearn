using ABCLearn.DAO;
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

        public IActionResult forgotPass()
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
            return RedirectToAction("Index", "Home");
        }
        private void renderData()
        {
            LecturerDAO.Instance.Lecturers();
            StudentDAO.Instance.Students();
            CourseDAO.Instance.Courses();
            QuizDAO.Instance.quizzes();
        }
        public IActionResult ResetPassword(string Password)
        {
            if (Profile.Instance.Role == "Student")
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
