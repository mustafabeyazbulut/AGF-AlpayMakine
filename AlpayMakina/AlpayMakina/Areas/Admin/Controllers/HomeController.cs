using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlpayMakina.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Home")]
    [Authorize]
    public class HomeController : Controller
    {
        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
