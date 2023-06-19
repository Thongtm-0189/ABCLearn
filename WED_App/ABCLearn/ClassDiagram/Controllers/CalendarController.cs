using ABCLearn.DataAccess;
using ABCLearn.DataContext;
using ABCLearn.Models;
using Microsoft.AspNetCore.Mvc;

namespace ABCLearn.Controllers
{
	public class CalendarController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
		public IActionResult SetTimeCalendar(Calendar calendar, string btnCalendar)
		{
			if (!UserLogin.Instance.Islogin || UserLogin.Instance.RoleID != "Admin")
			{
				return RedirectToAction("Index", "Admin");
			}
			if (btnCalendar == "Set")
			{
				calendar.EndTime = calendar.StartTime + (new TimeSpan(1, 30, 0));
				if (CalendarDAO.Instance.Update(calendar))
				{
					CalendarDAO.Instance.SaveChange();
					return RedirectToAction("Calendar", "Admin");
				}
			}
			else
			{
				if (CalendarDAO.Instance.Delete(calendar.Id))
				{
					CalendarDAO.Instance.SaveChange();
					return RedirectToAction("Calendar", "Admin");
				}
			}
			return RedirectToAction("Calendar", "Admin");
		}
		public IActionResult AddNewCalendar(int IDCourse, TimeSpan timeStart)
		{
			var timeEnd = timeStart + (new TimeSpan(01, 30, 00));
			var calendar = new Calendar() { IDCourse = IDCourse, StartTime = timeStart, EndTime = timeEnd };
			var isInser = CalendarDAO.Instance.AddNew(calendar);
			if (isInser)
			{
				CalendarDAO.Instance.SaveChange();
			}
			return RedirectToAction("Calendar", "Admin");
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
