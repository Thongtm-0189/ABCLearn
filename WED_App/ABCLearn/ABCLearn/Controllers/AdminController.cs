using ABCLearn.DataContext;
using ABCLearn.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;

namespace ABCLearn.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            renderData();
            return View();
        }

        public IActionResult Login(AccountLogin acc)
        {
            if (acc.Email == "" || acc.Password == "")
            {
                return RedirectToAction("Index", "Admin");
            }

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
        public IActionResult SetTimeCalendar(Calendar calendar, string btnCalendar)
        {
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
            return Calendar();
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
        public IActionResult ViewStudent(int idStudent)
        {
            Student student = StudentDAO.Instance.Students().Find(x => x.Id == idStudent);
            student.Courses.ForEach(x => x.Calendars = CourseDAO.Instance.getCalendar(x.Id));
            return View(student);
        }
        public IActionResult updateStudent(Profile pro)
        {
            StudentDAO.Instance.editprofile(pro);
            StudentDAO.Instance.upDate();
            return RedirectToAction("ViewStudent", "Admin", new { idStudent = pro.Id });
        }
        public IActionResult StudentAction(int idStudent, string btnStudentAdmin)
        {
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
            return RedirectToAction("pageStudent", "Admin", new { page = 0 });
        }
        public IActionResult ViewLecturer(int IDLecturer)
        {
            Lecturer lecturer = LecturerDAO.Instance.Lecturers().Find(x => x.Id == IDLecturer);
            lecturer.Courses.ForEach(x => x.Calendars = CourseDAO.Instance.getCalendar(x.Id));
            return View(lecturer);
        }
        public IActionResult removeLecturer(int idLecturer)
        {
            var isRemove = LecturerDAO.Instance.removeLecturer(idLecturer);
            if (isRemove)
            {
                CourseDAO.Instance.Update();
                LecturerDAO.Instance.Update();
            }
            return RedirectToAction("Lecturer", "Admin");
        }
        public IActionResult updateLecturer(Profile pro)
        {
            LecturerDAO.Instance.editprofile(pro);
            LecturerDAO.Instance.Update();
            return RedirectToAction("ViewLecturer", "Admin", new { IDLecturer = pro.Id });
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
        }
    }
}
