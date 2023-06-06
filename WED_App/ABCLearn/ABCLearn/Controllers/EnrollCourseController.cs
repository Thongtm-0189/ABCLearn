using ABCLearn.DataContext;
using ABCLearn.Extend;
using ABCLearn.Models;
using ABCLearn.Models.MOMO;
using ABCLearn.Models.VNPay;
using ABCLearn.Services;
using Microsoft.AspNetCore.Mvc;

namespace ABCLearn.Controllers
{
    public class EnrollCourseController : Controller
    {
        private readonly IVnPayService _vnPayService;
        private readonly IMomoService _momoService;
        private static Course course;
        private static bool paiedVNpay = false;
        public EnrollCourseController(IVnPayService vnPayService, IMomoService momoService)
        {
            _vnPayService = vnPayService;
            _momoService = momoService;
        }
        [HttpPost]
        public async Task<IActionResult> CreatePaymentUrlMOMO(int IdCourse)
        {
            course = CourseDAO.Instance.Courses().FirstOrDefault(x => x.Id == IdCourse);
            OrderInfoModel model = new OrderInfoModel();
            UserLogin user = UserLogin.Instance;
            model.FullName = user.FirstName + " " + user.LastName;
            model.Amount = Double.Parse(course.Price + "000");
            model.OrderInfo = $"{model.FullName} bought the course {course.Title} at ACBLearn";
            var response = await _momoService.CreatePaymentAsync(model);
            return Redirect(response.PayUrl);
        }
        [HttpPost]
        public IActionResult CreatePaymentUrlVNPAY(int IdCourse)
        {
            course = CourseDAO.Instance.Courses().FirstOrDefault(x => x.Id == IdCourse);
            PaymentInformationModel model = new PaymentInformationModel();
            UserLogin user = UserLogin.Instance;
            model.Name = user.FirstName + " " + user.LastName;
            model.Amount = Double.Parse(course.Price + "000");
            model.OrderDescription = $"{model.Name} bought the course {course.Title} at ACBLearn";
            var url = _vnPayService.CreatePaymentUrl(model, HttpContext);
            paiedVNpay = false;
            return Redirect(url);
        }
        public IActionResult SelectTypePay(int IdCourse, string paymentMethod)
        {
            var user = HttpContext.Session.GetObject<UserLogin>("User");
            if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }
            foreach (var course in user.Courses)
            {
                if (course.Id == IdCourse)
                {
                    return RedirectToAction("Profile", "Home");
                }
            }
            if (paymentMethod == "MOMO")
            {
                return RedirectToAction("CreatePaymentUrlMomo", "EnrollCourse", new { idcourse = IdCourse });
            }
            return CreatePaymentUrlVNPAY(IdCourse);
        }

        public async Task<IActionResult> CreatePaymentUrlMomo(int idcourse)
        {
            course = CourseDAO.Instance.Courses().FirstOrDefault(x => x.Id == idcourse);
            var user = HttpContext.Session.GetObject<UserLogin>("User");
            OrderInfoModel model = new OrderInfoModel();
            model.FullName = user.FirstName + " " + user.LastName;
            model.Amount = Double.Parse(course.Price + "000");
            model.OrderInfo = $"{model.FullName} bought the course {course.Title} at ACBLearn";
            var response = await _momoService.CreatePaymentAsync(model);
            return Redirect(response.PayUrl);
        }

        public IActionResult PaymentCallbackVNpay()
        {
            var user = HttpContext.Session.GetObject<UserLogin>("User");
            var response = _vnPayService.PaymentExecute(Request.Query);
            if (paiedVNpay)
            {
                return RedirectToAction("Index", "Home");
            }
            if (response.TransactionId == "0")
            {
                ViewBag.check = "Fail!!";
            }
            else
            {
                paiedVNpay = true;
                CourseDAO.Instance.enrollCourse(course.Id, user.Id);
                CourseDAO.Instance.Update();
                ViewBag.check = "Success";
            }
            SessionUser();
            return View(response);
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
        }
        public IActionResult PaymentCallBackMomo()
        {
            var response = _momoService.PaymentExecuteAsync(HttpContext.Request.Query);
            if (response.ErrorCode == 0)
            {
                ViewBag.check = "thanh cong";
            }
            else
            {
                ViewBag.check = "that bai";
            }
            return View(response);
        }
    }
}
