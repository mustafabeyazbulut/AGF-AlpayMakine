using Microsoft.AspNetCore.Mvc;

namespace AlpayMakina.Controllers
{
 
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Home", new { area = "UI" });
        }
    }
}
