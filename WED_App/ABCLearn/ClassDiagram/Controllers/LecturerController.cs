using ABCLearn.DataContext;
using ABCLearn.Models;
using Microsoft.AspNetCore.Mvc;

namespace ABCLearn.Controllers
{
    public class LecturerController : Controller
    {
        private readonly IWebHostEnvironment _environment;
        public LecturerController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddNewLecturer(Profile lecturer, IFormFile image)
        {
            if (!UserLogin.Instance.Islogin || UserLogin.Instance.RoleID != "Admin")
            {
                return RedirectToAction("Index", "Admin");
            }
            var checkInsert = LecturerDAO.Instance.AddNew(lecturer);
            if (checkInsert)
            {
                LecturerDAO.Instance.SaveChange();
                Lecturer les = LecturerDAO.Instance.Lecturers().Find(l => l.Password == lecturer.Password && l.Email == lecturer.Email);
                if (image != null && image.Length > 0)
                {
                    var fileName = $"Lecturer-{les.Id}{Path.GetExtension(image.FileName)}";
                    var path = Path.Combine(_environment.WebRootPath, "img\\Avatar\\Lecturer", fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        image.CopyTo(fileStream);
                    }
                    LecturerDAO.Instance.uploadAvatar(fileName, les.Id);
                    LecturerDAO.Instance.SaveChange();
                }
            }
            return RedirectToAction("Lecturer", "Admin");
        }
        public IActionResult ViewLecturer(int IDLecturer)
        {
            if (!UserLogin.Instance.Islogin || UserLogin.Instance.RoleID != "Admin")
            {
                return RedirectToAction("Index", "Admin");
            }
            Lecturer lecturer = LecturerDAO.Instance.Lecturers().Find(x => x.Id == IDLecturer);
            lecturer.Courses.ForEach(x => x.Calendars = CourseDAO.Instance.getCalendar(x.Id));
            return View("Views/Admin/ViewLecturer.cshtml", lecturer);
        }
        public IActionResult removeLecturer(int idLecturer)
        {
            if (!UserLogin.Instance.Islogin || UserLogin.Instance.RoleID != "Admin")
            {
                return RedirectToAction("Index", "Admin");
            }
            var isRemove = LecturerDAO.Instance.Delete(idLecturer);
            if (isRemove)
            {
                CourseDAO.Instance.SaveChange();
                LecturerDAO.Instance.SaveChange();
            }
            return RedirectToAction("Lecturer", "Admin");
        }
        public IActionResult updateLecturer(Profile pro)
        {
            LecturerDAO.Instance.Update(pro);
            LecturerDAO.Instance.SaveChange();
            CourseDAO.Instance.SaveChange();
            return RedirectToAction("ViewLecturer", "Lecturer", new { IDLecturer = pro.Id });
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
