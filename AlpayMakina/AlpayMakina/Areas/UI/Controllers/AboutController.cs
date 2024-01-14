using Microsoft.AspNetCore.Mvc;

namespace AlpayMakina.Areas.UI.Controllers
{
    [Area("UI")]
    [Route("UI/About")]
    public class AboutController : Controller
	{
        [Route("")]
        [Route("Index")]
        public IActionResult Index()
		{
			return View();
		}
	}
}
