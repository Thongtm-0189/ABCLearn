using ABCLearn.DataAccess;
using ABCLearn.Extend;
using ABCLearn.Models;
using Microsoft.AspNetCore.Mvc;

namespace ABCLearn.Controllers
{
	public class NotificationController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
		public IActionResult addnewNotification(Notification notification)
		{
			NotificationDAO.Instanse.AddNew(notification);
			NotificationDAO.Instanse.SaveChange();
			return RedirectToAction("Nontification", "Admin");
		}
		public IActionResult delete(int idNotification)
		{
			NotificationDAO.Instanse.Delete(idNotification);
			NotificationDAO.Instanse.SaveChange();
			return RedirectToAction("Nontification", "Admin");
		}

		public IActionResult ViewNotification()
		{
			SessionUser();
			return View("Views/Home/Notification.cshtml");
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
				ViewBag.Role = "guest";
				ViewBag.login = false;
			}
		}
	}
}
