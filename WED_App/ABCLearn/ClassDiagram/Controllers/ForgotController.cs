using ABCLearn.Models;
using ABCLearn.Services;
using Microsoft.AspNetCore.Mvc;

namespace ABCLearn.Controllers
{
	public class ForgotController : Controller
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
	}
}
