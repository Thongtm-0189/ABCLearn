using ABCLearn.DataContext;
using ABCLearn.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ABCLearn.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            renderData();
            return View("Views/Admin/Index.cshtml");
        }
        public IActionResult Login(AccountLogin acc)
        {
            if (AdminDAO.Instence().Login(acc))
            {
                UserLogin.Instance.Email = acc.Email;
                UserLogin.Instance.Password = acc.Password;
                UserLogin.Instance.RoleID = "ADmin";
                UserLogin.Instance.Islogin = true;
                return RedirectToAction("Student", "Admin", new { page = 0 });
            }
            return RedirectToAction("Index", "Admin");
        }
        public IActionResult Lecturer(int page = 0, int max = 1)
        {
            if (!UserLogin.Instance.Islogin)
            {
                return RedirectToAction("Index", "Admin");
            }

            List<Lecturer> subList = new List<Lecturer>();
            if (page != (max - 1))
            {
                subList = LecturerDAO.Instance.Lecturers().GetRange(8 * page, 8);
            }
            else
            {
                subList = LecturerDAO.Instance.Lecturers().Skip(8 * page).ToList();
            }
            return View(subList);
        }
        public IActionResult Course(int page = 0, int max = 1)
        {
            if (!UserLogin.Instance.Islogin)
            {
                return RedirectToAction("Index", "Admin");
            }

            List<Course> subList = new List<Course>();
            if (page != (max - 1))
            {
                subList = CourseDAO.Instance.Courses().GetRange(8 * page, 8);
            }
            else
            {
                subList = CourseDAO.Instance.Courses().Skip(8 * page).ToList();
            }
            return View(subList);
        }
        public IActionResult Calendar()
        {
            if (!UserLogin.Instance.Islogin)
            {
                return RedirectToAction("Index", "Admin");
            }

            return View();
        }
        public IActionResult Nontification()
        {
            if (!UserLogin.Instance.Islogin)
            {
                return RedirectToAction("Index", "Admin");
            }

            return View();
        }
        public IActionResult Student(int page = 0, int max = 0)
        {
            if (!UserLogin.Instance.Islogin)
            {
                return RedirectToAction("Index", "Admin");
            }

            List<Student> subList = new List<Student>();
            if (page != (max - 1))
            {
                subList = StudentDAO.Instance.Students().GetRange(8 * page, 8);
            }
            else
            {
                subList = StudentDAO.Instance.Students().Skip(8 * page).ToList();
            }
            return View(@"Views/Admin/AdminPage.cshtml", subList);
        }

        public IActionResult StudentAction(int idStudent, string btnStudentAdmin)
        {
            switch (btnStudentAdmin)
            {
                case "View":
                    break;
                case "Delete":
                    var isRemove = StudentDAO.Instance.removeStudent(idStudent);
                    if (isRemove)
                    {
                        StudentDAO.Instance.upDate();
                    }
                    break;
            }
            return RedirectToAction("pageStudent", "Admin", new { page = 0 });
        }
        public IActionResult LecturertAction(int countCourse, int idLecturer, string btnLecturerAdmin)
        {
            switch (btnLecturerAdmin)
            {
                case "View":
                    break;
                case "Delete":
                    if (countCourse == 0)
                    {
                        var isRemove = LecturerDAO.Instance.removeLecturer(idLecturer);
                        if (isRemove)
                        {
                            LecturerDAO.Instance.Update();
                        }
                    }
                    else
                    {
                        TempData["Lecturer"] = "Lecturer still teaching!! can't remove";
                    }
                    break;
            }
            return RedirectToAction("Lecturer", "Admin");
        }

        public IActionResult LogOut()
        {
            UserLogin.Instance.Email = "";
            UserLogin.Instance.Password = "";
            UserLogin.Instance.RoleID = "";
            UserLogin.Instance.Islogin = false;
            return RedirectToAction("Index", "Admin");
        }
        private void renderData()
        {
            LecturerDAO.Instance.Lecturers();
            StudentDAO.Instance.Students();
            CourseDAO.Instance.Courses();
            QuizDAO.Instance.quizzes();
        }
    }
}
