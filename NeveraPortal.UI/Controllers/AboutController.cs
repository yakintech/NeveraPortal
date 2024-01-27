using Microsoft.AspNetCore.Mvc;

namespace NeveraPortal.UI.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "About";
            return View();
        }
    }
}
