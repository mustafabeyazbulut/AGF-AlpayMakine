using Microsoft.AspNetCore.Mvc;

namespace AlpayMakina.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
