using AlpayMakina.Repositories.SliderRepositories;
using Microsoft.AspNetCore.Mvc;

namespace AlpayMakina.Areas.UI.ViewComponents
{
    public class _Slider_Home_ComponentPartial : ViewComponent
    {
        private readonly ISliderRepository _sliderRepository;

        public _Slider_Home_ComponentPartial(ISliderRepository sliderRepository)
        {
            _sliderRepository = sliderRepository;
        }

        public async Task< IViewComponentResult> InvokeAsync()
        {
            var values= await _sliderRepository.GetAllSliderAsync();
            return View(values);
        }
    }
}
