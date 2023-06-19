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
					DateTime nowUtc = DateTime.UtcNow;
					TimeZoneInfo vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"); // Múi giờ của Việt Nam
					DateTime vietnamTime = TimeZoneInfo.ConvertTimeFromUtc(nowUtc, vietnamTimeZone);
					var check = (vietnamTime - UserLogin.Instance.DateCreated);
					if (check.TotalDays >= 30 || !UserLogin.Instance.IsConfirmEmail)
					{
						return RedirectToAction("ConfirmAcceptAccout", "Register", new { email = UserLogin.Instance.Email, forgot = UserLogin.Instance.Id });
					}
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

		public IActionResult Logout()
		{
			UserLogin.Instance.Islogin = false;
			UserLogin.Instance.RoleID = "Guest";
			HttpContext.Session.Clear();
			return RedirectToAction("Index", "Home");
		}
		private void renderData()
		{
			LecturerDAO.Instance.GetAll();
			StudentDAO.Instance.GetAll();
			CourseDAO.Instance.GetAll();
			QuizDAO.Instance.GetAll();
		}
		public IActionResult ResetPassword(string Password)
		{
			var role = HttpContext.Session.GetString("role");
			if (role == "Student")
			{
				Profile.Instance.Password = Password;
				if (StudentDAO.Instance.resetPassword(Profile.Instance))
				{
					Profile.Instance = null;
					TempData["MessSS"] = "Success!!";
					StudentDAO.Instance.SaveChange();
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
					LecturerDAO.Instance.SaveChange();
					return RedirectToAction("Index", "Login");
				}
			}
			TempData["MessError"] = "ERROR in reset password!!";
			return RedirectToAction("Index", "Login");
		}
	}
}
