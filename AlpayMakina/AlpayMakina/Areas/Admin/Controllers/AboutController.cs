using AlpayMakina.Dtos.AboutDtos;
using AlpayMakina.Dtos.ProductDtos;
using AlpayMakina.Repositories.AboutRepositories;
using AlpayMakina.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlpayMakina.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/About")]
    [Authorize]
    public class AboutController : Controller
    {
        private readonly IAboutRepository _AboutRepository;
        private readonly ImageOperations _imageOperations;

        public AboutController(IAboutRepository AboutRepository, ImageOperations imageOperations)
        {
            _AboutRepository = AboutRepository;
            _imageOperations = imageOperations;
        }
        [Route("")]
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var values = await _AboutRepository.GetAllAboutAsync();
            return View(values);
        }

        [Route("CreateAbout")]
        public IActionResult CreateAbout()
        {
            return View();
        }

        [HttpPost]
        [Route("CreateAbout")]
        public async Task<IActionResult> CreateAbout(CreateAboutDto createAboutDto)
        {
            createAboutDto.HDate = DateTime.Now;
            createAboutDto.HTime = DateTime.Now.TimeOfDay;
            _imageOperations.FilePath = "AboutPhoto";
            if (createAboutDto.ImageFile != null && createAboutDto.ImageFile.Length > 0)
            {
                createAboutDto.ImageUrl = await _imageOperations.UploadImageAsync(createAboutDto.ImageFile);
            }
            await _AboutRepository.AddAboutAsync(createAboutDto);

            return RedirectToAction("Index", "About", new { area = "Admin" });
        }
        [Route("RemoveAbout/{id}")]
        public async Task<IActionResult> RemoveAbout(int id)
        {
            _imageOperations.FilePath = "AboutPhoto";
            var lastAbout = await _AboutRepository.GetAboutAsync(id);

            await _imageOperations.DeleteIconAsync(lastAbout.ImageUrl);

            await _AboutRepository.RemoveAboutAsync(id);

            return RedirectToAction("Index", "About", new { area = "Admin" });
        }

        [HttpGet]
        [Route("UpdateAbout/{id}")]
        public async Task<IActionResult> UpdateAbout(int id)
        {
            var value = await _AboutRepository.GetAboutAsync(id);

            return View(value);
        }

        [HttpPost]
        [Route("UpdateAbout/{id}")]
        public async Task<IActionResult> UpdateAbout(UpdateAboutDto updateAboutDto)
        {
            updateAboutDto.HDate = DateTime.Now;
            updateAboutDto.HTime = DateTime.Now.TimeOfDay;

            _imageOperations.FilePath = "AboutPhoto";

            if (updateAboutDto.ImageFile != null && updateAboutDto.ImageFile.Length > 0)
            {
                var lastAbout = await _AboutRepository.GetAboutAsync(updateAboutDto.Id);
                bool test = await _imageOperations.DeleteIconAsync(lastAbout.ImageUrl);
                updateAboutDto.ImageUrl = await _imageOperations.UploadImageAsync(updateAboutDto.ImageFile);

            }
            else
            {
                var lastAbout = await _AboutRepository.GetAboutAsync(updateAboutDto.Id);
                updateAboutDto.ImageUrl = lastAbout.ImageUrl;
            }
            await _AboutRepository.UpdateAboutAsync(updateAboutDto);

            return RedirectToAction("Index", "About", new { area = "Admin" });

        }
    }
}
