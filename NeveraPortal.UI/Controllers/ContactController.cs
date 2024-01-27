using Microsoft.AspNetCore.Mvc;

namespace NeveraPortal.UI.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "Contact";
            return View();
        }
    }
}
