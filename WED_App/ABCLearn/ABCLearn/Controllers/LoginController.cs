using Microsoft.AspNetCore.Mvc;

namespace ABCLearn.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
