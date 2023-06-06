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
                if (CourseDAO.Instance.setCalendar(calendar))
                {
                    CourseDAO.Instance.Update();
                    return RedirectToAction("Calendar", "Admin");
                }
            }
            else
            {
                if (CourseDAO.Instance.removeCalendar(calendar))
                {
                    CourseDAO.Instance.Update();
                    return RedirectToAction("Calendar", "Admin");
                }
            }
            return RedirectToAction("Calendar", "Admin");
        }
        public IActionResult AddNewCalendar(int IDCourse, TimeSpan timeStart)
        {
            var timeEnd = timeStart + (new TimeSpan(01, 30, 00));
            var isInser = CourseDAO.Instance.AddNewCalendar(IDCourse, timeStart, timeEnd);
            if (isInser)
            {
                CourseDAO.Instance.Update();
            }
            return RedirectToAction("Calendar", "Admin");
        }
        private void renderData()
        {
            LecturerDAO.Instance.getLecturer();
            StudentDAO.Instance.getStudents();
            CourseDAO.Instance.getCourse();
            QuizDAO.Instance.GetQuiz();
        }
    }
}
