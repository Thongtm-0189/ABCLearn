using ABCLearn.Extend;
using ABCLearn.Models;
using Microsoft.AspNetCore.Mvc;

namespace ABCLearn.Controllers
{
	public class ErrorController : Controller
	{
		public IActionResult NotFound()
		{
			Response.StatusCode = 404;
			SessionUser();
			return View();
		}
		public IActionResult Index()
		{
			return View();
		}
		private void SessionUser()
		{
			var user = HttpContext.Session.GetObject<UserLogin>("User");
			if (user != null)
			{
				ViewBag.Role = user.RoleID;
				ViewBag.login = true;
				ViewBag.user = user;
			}
			else
			{
				ViewBag.Role = "Guest";
				ViewBag.login = false;
			}
		}
	}
}
