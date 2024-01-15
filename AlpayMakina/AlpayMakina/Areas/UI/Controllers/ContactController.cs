using AlpayMakina.Repositories.ContactRepositories;
using Microsoft.AspNetCore.Mvc;

namespace AlpayMakina.Areas.UI.Controllers
{
    [Area("UI")]
    [Route("UI/Contact")]
    public class ContactController : Controller
	{
        private readonly IContactRepository _contactRepository;

        public ContactController(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        [Route("")]
        [Route("Index")]
        public async Task<IActionResult> Index()
		{
            var values=await _contactRepository.GetAllContactAsync();
			return View(values);
		}
	}
}
