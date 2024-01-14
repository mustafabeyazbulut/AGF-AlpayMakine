using AlpayMakina.Dtos.SocialMediaDtos;
using AlpayMakina.Repositories.SocialMediaRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace AlpayMakina.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/SocialMedia")]
    [Authorize]
    public class SocialMediaController : Controller
    {
        private readonly ISocialMediaRepository _socialMediaRepository;

        public SocialMediaController(ISocialMediaRepository socialMediaRepository)
        {
            _socialMediaRepository = socialMediaRepository;
        }
        [Route("")]
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var values = await _socialMediaRepository.GetAllSocialMediaAsync();
            return View(values);
        }

        [Route("CreateSocialMedia")]
        public IActionResult CreateSocialMedia()
        {
            return View();
        }

        [HttpPost]
        [Route("CreateSocialMedia")]
        public async Task<IActionResult> CreateSocialMedia(CreateSocialMediaDto createSocialMediaDto)
        {
            await _socialMediaRepository.AddSocialMediaAsync(createSocialMediaDto);

            return RedirectToAction("Index", "SocialMedia", new { area = "Admin" });
        }
        [Route("RemoveSocialMedia/{id}")]
        public async Task<IActionResult> RemoveSocialMedia(int id)
        {
            await _socialMediaRepository.RemoveSocialMediaAsync(id);

            return RedirectToAction("Index", "SocialMedia", new { area = "Admin" });
        }

        [HttpGet]
        [Route("UpdateSocialMedia/{id}")]
        public async Task<IActionResult> UpdateSocialMedia(int id)
        {
            var value= await _socialMediaRepository.GetSocialMediaAsync(id);

            return View(value);
        }

        [HttpPost]
        [Route("UpdateSocialMedia/{id}")]
        public async Task<IActionResult> UpdateSocialMedia(UpdateSocialMediaDto updateSocialMediaDto)
        {
            await _socialMediaRepository.UpdateSocialMediaAsync(updateSocialMediaDto);
            
            return RedirectToAction("Index", "SocialMedia", new { area = "Admin" });
           
        }
    }
}
