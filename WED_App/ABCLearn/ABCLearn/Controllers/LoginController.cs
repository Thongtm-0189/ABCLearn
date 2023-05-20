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
                    UserLogin.Instance.Courses = StudentDAO.Instance.GetCourses(UserLogin.Instance.Id);
                    UserLogin.Instance.Islogin = true;
                    return RedirectToAction("Index", "Home");

                }
                else
                {
                    ViewBag.MessError = "Pass Or Email was Wrong!!";
                    return RedirectToAction("Index", "Login");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public IActionResult Logout()
        {
            UserLogin.Instance.Islogin = false;
            return RedirectToAction("Index", "Home");
        }
    }
}
