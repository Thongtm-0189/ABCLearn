using Microsoft.AspNetCore.Mvc;

namespace ABCLearn.Controllers
{
    public class DictionaryController : Controller
    {
        public IActionResult Index(string searchDictionary)
        {
            ViewBag.word = searchDictionary;
            return View();
        }
    }
}
