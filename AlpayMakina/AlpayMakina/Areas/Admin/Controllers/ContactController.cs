using AlpayMakina.Dtos.ContactDtos;
using AlpayMakina.Repositories.ContactRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlpayMakina.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Route("Admin/Contact")]
	[Authorize]
	public class ContactController : Controller
	{
		private readonly IContactRepository _ContactRepository;

		public ContactController(IContactRepository ContactRepository)
		{
			_ContactRepository = ContactRepository;
		}
		[Route("")]
		[Route("Index")]
		public async Task<IActionResult> Index()
		{
			var values = await _ContactRepository.GetAllContactAsync();
			return View(values);
		}

		[Route("CreateContact")]
		public IActionResult CreateContact()
		{
			return View();
		}

		[HttpPost]
		[Route("CreateContact")]
		public async Task<IActionResult> CreateContact(CreateContactDto createContactDto)
		{
			await _ContactRepository.AddContactAsync(createContactDto);

			return RedirectToAction("Index", "Contact", new { area = "Admin" });
		}
		[Route("RemoveContact/{id}")]
		public async Task<IActionResult> RemoveContact(int id)
		{
			await _ContactRepository.RemoveContactAsync(id);

			return RedirectToAction("Index", "Contact", new { area = "Admin" });
		}

		[HttpGet]
		[Route("UpdateContact/{id}")]
		public async Task<IActionResult> UpdateContact(int id)
		{
			var value = await _ContactRepository.GetContactAsync(id);

			return View(value);
		}

		[HttpPost]
		[Route("UpdateContact/{id}")]
		public async Task<IActionResult> UpdateContact(UpdateContactDto updateContactDto)
		{
			await _ContactRepository.UpdateContactAsync(updateContactDto);

			return RedirectToAction("Index", "Contact", new { area = "Admin" });

		}
	}
}
