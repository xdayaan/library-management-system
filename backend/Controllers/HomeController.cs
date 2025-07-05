using Microsoft.AspNetCore.Mvc;

namespace LMS.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Librarian");
            }
            return RedirectToAction("Index", "Record");
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
