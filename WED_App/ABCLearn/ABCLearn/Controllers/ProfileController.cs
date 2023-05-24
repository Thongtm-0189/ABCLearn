using ABCLearn.DataContext;
using ABCLearn.Models;
using Microsoft.AspNetCore.Mvc;

namespace ABCLearn.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IWebHostEnvironment _environment;

        public ProfileController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult EditInformation(Profile pro)
        {
            if (UserLogin.Instance.RoleID.Trim() == "Student")
            {
                bool checkEdited = StudentDAO.Instance.editprofile(pro);
                if (checkEdited)
                {
                    int idUserLgin = UserLogin.Instance.Id;
                    UserLogin.Instance.RoleID = "Student";
                    UserLogin.updateIfor(idUserLgin, "Student");
                }
            }
            else
            {
                bool checkEdited = LecturerDAO.Instance.editprofile(pro);
                if (checkEdited)
                {
                    UserLogin.Instance.RoleID = "Lecturer";
                    int idUserLgin = UserLogin.Instance.Id;
                    UserLogin.updateIfor(idUserLgin, "Lecturer");
                }
            }
            UserLogin.Instance.Courses.ForEach(course => course.Calendars = CourseDAO.Instance.getCalendar(course.Id));
            return View("Views/Home/Profile.cshtml");
        }
        public IActionResult UploadAvatar(IFormFile image)
        {
            var roleID = UserLogin.Instance.RoleID;
            var id = UserLogin.Instance.Id;
            if (image != null && image.Length > 0)
            {
                ViewBag.fail = "not fail";
                var fileName = $"{roleID}-{id}{Path.GetExtension(image.FileName)}";
                //var fileName = Path.GetFileName(image.FileName);
                var path = Path.Combine(_environment.WebRootPath, "img\\Avatar\\Student", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    image.CopyTo(fileStream);
                }
                StudentDAO.Instance.uploadAvatar(fileName);
                UserLogin.Instance.Avatar = fileName;
            }
            else
            {
                ViewBag.fail = "fail";
            }
            return View("Views/Home/ProFile.cshtml");
        }
    }
}
