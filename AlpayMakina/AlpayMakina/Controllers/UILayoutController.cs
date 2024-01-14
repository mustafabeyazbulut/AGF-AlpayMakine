using Microsoft.AspNetCore.Mvc;

namespace AlpayMakina.Controllers
{
    public class UILayoutController : Controller
    {
        public IActionResult Index()
        {
            
            return View();
        }
    }
}
