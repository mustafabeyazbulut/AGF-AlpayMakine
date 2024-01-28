using AlpayMakina.Dtos.ProductDtos;
using AlpayMakina.Dtos.SliderDtos;
using AlpayMakina.Repositories.SliderRepositories;
using AlpayMakina.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlpayMakina.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Slider")]
    [Authorize]
    public class SliderController : Controller
    {
        private readonly ISliderRepository _SliderRepository;
        private readonly ImageOperations _ImageOperations;

        public SliderController(ISliderRepository SliderRepository, ImageOperations imageOperations)
        {
            _SliderRepository = SliderRepository;
            _ImageOperations = imageOperations;
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
            if (string.IsNullOrEmpty(createSliderDto.Active))
            {
                createSliderDto.Active = "";
            }

            _ImageOperations.FilePath = "SliderPhoto";

            if (createSliderDto.ProductImageFile != null && createSliderDto.ProductImageFile.Length > 0)
            {
                createSliderDto.ImageUrl = await _ImageOperations.UploadImageAsync(createSliderDto.ProductImageFile);
            }

            if (createSliderDto.PriceImageFile != null && createSliderDto.PriceImageFile.Length > 0)
            {
                createSliderDto.PriceUrl = await _ImageOperations.UploadImageAsync(createSliderDto.PriceImageFile);
            }

            await _SliderRepository.AddSliderAsync(createSliderDto);

            return RedirectToAction("Index", "Slider", new { area = "Admin" });
        }
        [Route("RemoveSlider/{id}")]
        public async Task<IActionResult> RemoveSlider(int id)
        {
            _ImageOperations.FilePath = "SliderPhoto";
            var lastProduct = await _SliderRepository.GetSliderAsync(id);
            await _ImageOperations.DeleteIconAsync(lastProduct.ImageUrl);
            await _ImageOperations.DeleteIconAsync(lastProduct.PriceUrl);

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
            if (string.IsNullOrEmpty(updateSliderDto.Active))
            {
                updateSliderDto.Active = "";
            }
            _ImageOperations.FilePath = "SliderPhoto";
            if (updateSliderDto.ProductImageFile != null && updateSliderDto.ProductImageFile.Length > 0)
            {
                var lastProduct = await _SliderRepository.GetSliderAsync(updateSliderDto.Id);
                bool test = await _ImageOperations.DeleteIconAsync(lastProduct.ImageUrl);

                updateSliderDto.ImageUrl = await _ImageOperations.UploadImageAsync(updateSliderDto.ProductImageFile);
            }
            else
            {
                var lastProduct = await _SliderRepository.GetSliderAsync(updateSliderDto.Id);
                updateSliderDto.ImageUrl = lastProduct.ImageUrl;
            }

            if (updateSliderDto.PriceImageFile != null && updateSliderDto.PriceImageFile.Length > 0)
            {
                var lastProduct = await _SliderRepository.GetSliderAsync(updateSliderDto.Id);
                bool test = await _ImageOperations.DeleteIconAsync(lastProduct.PriceUrl);

                updateSliderDto.PriceUrl = await _ImageOperations.UploadImageAsync(updateSliderDto.PriceImageFile);
            }
            else
            {
                var lastProduct = await _SliderRepository.GetSliderAsync(updateSliderDto.Id);
                updateSliderDto.PriceUrl = lastProduct.PriceUrl;
            }



            await _SliderRepository.UpdateSliderAsync(updateSliderDto);

            return RedirectToAction("Index", "Slider", new { area = "Admin" });

        }
    }
}
