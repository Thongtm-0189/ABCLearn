using Microsoft.AspNetCore.Mvc;
using ABCLearn.Models;
using ABCLearn.DataContext;
namespace ABCLearn.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult searchStudentByLastname(string LastName)
        {
            List<Student> list = StudentDAO.Instance.Students().Where(x => x.LastName.Contains(LastName)).ToList();
            return View("Views/Admin/AdminPage.cshtml", list);
        }
        public IActionResult StudentAction(int idStudent, string btnStudentAdmin)
        {
            if (!UserLogin.Instance.Islogin || UserLogin.Instance.RoleID != "Admin")
            {
                return RedirectToAction("Index", "Admin");
            }
            if (btnStudentAdmin == "View")
            {

            }
            else
            {
                var isRemove = StudentDAO.Instance.removeStudent(idStudent);
                if (isRemove)
                {
                    StudentDAO.Instance.upDate();
                }
            }
            return RedirectToAction("Student", "Admin", new { page = 0 });
        }
        public IActionResult ViewStudent(int idStudent)
        {
            if (!UserLogin.Instance.Islogin || UserLogin.Instance.RoleID != "Admin")
            {
                return RedirectToAction("Index", "Admin");
            }
            if (!UserLogin.Instance.Islogin)
            {
                return RedirectToAction("Index", "Admin");
            }
            Student student = StudentDAO.Instance.Students().Find(x => x.Id == idStudent);
            student.Courses.ForEach(x => x.Calendars = CourseDAO.Instance.getCalendar(x.Id));
            return View("Views/Admin/ViewStudent.cshtml", student);
        }
        public IActionResult updateStudent(Profile pro)
        {
            if (!UserLogin.Instance.Islogin || UserLogin.Instance.RoleID != "Admin")
            {
                return RedirectToAction("Index", "Admin");
            }
            StudentDAO.Instance.editprofile(pro);
            StudentDAO.Instance.upDate();
            return RedirectToAction("ViewStudent", "Student", new { idStudent = pro.Id });
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
