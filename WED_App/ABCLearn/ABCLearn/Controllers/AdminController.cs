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
            return View("Views/Admin/Index.cshtml");
        }
        public IActionResult Login(AccountLogin acc)
        {
            if (AdminDAO.Instence().Login(acc))
                return View(@"Views/Admin/AdminPage.cshtml");
            else
            {
                return View("Views/Admin/Index.cshtml");
            }

        }
    }
}
