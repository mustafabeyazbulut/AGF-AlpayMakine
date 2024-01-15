using AlpayMakina.Repositories.AboutRepositories;
using Microsoft.AspNetCore.Mvc;

namespace AlpayMakina.Areas.UI.Controllers
{
    [Area("UI")]
    [Route("UI/About")]
    public class AboutController : Controller
	{
        private readonly IAboutRepository _aboutRepository;

        public AboutController(IAboutRepository aboutRepository)
        {
            _aboutRepository = aboutRepository;
        }

        [Route("")]
        [Route("Index")]
        public async Task< IActionResult> Index()
		{
            var values = await _aboutRepository.GetAllAboutAsync();

			return View(values);
		}
	}
}
