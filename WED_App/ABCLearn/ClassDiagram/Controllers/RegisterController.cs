using ABCLearn.DataContext;
using ABCLearn.Models;
using ABCLearn.Services;
using Microsoft.AspNetCore.Mvc;

namespace ABCLearn.Controllers
{
	public class RegisterController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
		public IActionResult ConfirmEmail(Profile pro, string forgot)
		{
			if (pro != null)
			{
				HttpContext.Session.SetString("role", pro.Role);
				Profile.Instance = pro;
				string email = pro.Email;
				(bool, int) afterSend = Email.Instance.sendOTP(email, forgot);
				bool isSend = afterSend.Item1;
				int OTP = afterSend.Item2;
				StudentDAO.Instance.AddNew(pro);
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
		public IActionResult ConfirmAcceptAccout(string email, string forgot)
		{
			(bool, int) afterSend = Email.Instance.sendOTP(email);
			bool isSend = afterSend.Item1;
			int OTP = afterSend.Item2;
			if (isSend)
			{
				ViewBag.Email = email;
				ViewBag.OTP = OTP;
				return View("Views/Login/ConfirmEmail.cshtml", forgot);
			}
			TempData["MessError"] = "ERROR in confirm form let try again!!";
			return RedirectToAction("Index", "Login");
		}
		public IActionResult AcceptanceEmail(string forgot, int id = 0)
		{
			if (forgot == "reset")
			{
				return RedirectToAction("forgotPass", "Login");
			}
			else
			{
				if (id != 0)
				{
					StudentDAO.Instance.confirmAccountUser(id);
				}
			}
			TempData["MessSS"] = "Success!!";
			StudentDAO.Instance.SaveChange();
			return RedirectToAction("Index", "Login");
		}
	}
}
