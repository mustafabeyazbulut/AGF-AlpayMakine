using AlpayMakina.Repositories.ProductRepositories;
using Microsoft.AspNetCore.Mvc;

namespace AlpayMakina.Areas.UI.ViewComponents
{
	public class _Products_Home_ComponentPartial:ViewComponent
	{
		private readonly IProductRepository _productRepository;

        public _Products_Home_ComponentPartial(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task< IViewComponentResult> InvokeAsync()
		{
			var values = await _productRepository.GetFeatureProductAsync();
			return View(values);
		}
	}
}
