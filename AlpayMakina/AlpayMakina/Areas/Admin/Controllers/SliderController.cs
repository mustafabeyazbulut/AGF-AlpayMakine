using AlpayMakina.Dtos.SliderDtos;
using AlpayMakina.Repositories.SliderRepositories;
using Microsoft.AspNetCore.Mvc;

namespace AlpayMakina.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Slider")]
    public class SliderController : Controller
    {
        private readonly ISliderRepository _SliderRepository;

        public SliderController(ISliderRepository SliderRepository)
        {
            _SliderRepository = SliderRepository;
        }
        [Route("")]
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var values = await _SliderRepository.GetAllSliderAsync();
            return View(values);
        }

        [Route("CreateSlider")]
        public IActionResult CreateSlider()
        {
            return View();
        }

        [HttpPost]
        [Route("CreateSlider")]
        public async Task<IActionResult> CreateSlider(CreateSliderDto createSliderDto)
        {
            await _SliderRepository.AddSliderAsync(createSliderDto);

            return RedirectToAction("Index", "Slider", new { area = "Admin" });
        }
        [Route("RemoveSlider/{id}")]
        public async Task<IActionResult> RemoveSlider(int id)
        {
            await _SliderRepository.RemoveSliderAsync(id);

            return RedirectToAction("Index", "Slider", new { area = "Admin" });
        }

        [HttpGet]
        [Route("UpdateSlider/{id}")]
        public async Task<IActionResult> UpdateSlider(int id)
        {
            var value = await _SliderRepository.GetSliderAsync(id);

            return View(value);
        }

        [HttpPost]
        [Route("UpdateSlider/{id}")]
        public async Task<IActionResult> UpdateSlider(UpdateSliderDto updateSliderDto)
        {
            await _SliderRepository.UpdateSliderAsync(updateSliderDto);

            return RedirectToAction("Index", "Slider", new { area = "Admin" });

        }
    }
}
