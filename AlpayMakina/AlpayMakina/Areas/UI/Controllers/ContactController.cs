using Microsoft.AspNetCore.Mvc;

namespace AlpayMakina.Areas.UI.Controllers
{
    [Area("UI")]
    [Route("UI/Contact")]
    public class ContactController : Controller
	{
        [Route("")]
        [Route("Index")]
        public IActionResult Index()
		{
			return View();
		}
	}
}
