using ABCLearn.Services;
using Microsoft.AspNetCore.Mvc;

namespace ABCLearn.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index(string email, string name, string contact, string subject)
        {
            bool checkSendEmailContact = Email.Instance.sendContact(email, name, contact, subject);
            if (checkSendEmailContact)
            {
                ViewBag.checkContact = "Thank your Contact!!";
            }
            return View("Views/Home/Contact.cshtml");
        }
    }
}
