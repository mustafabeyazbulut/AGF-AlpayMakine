using AlpayMakina.Repositories.ProductRepositories;
using Microsoft.AspNetCore.Mvc;

namespace AlpayMakina.Areas.UI.ViewComponents
{
	public class _Products_ComponentPartial:ViewComponent
	{
		private readonly IProductRepository _productRepository;

		public _Products_ComponentPartial(IProductRepository productRepository)
		{
			_productRepository = productRepository;
		}
		public async Task<IViewComponentResult> InvokeAsync(int CategoryId=0,int SubCategoryId=0)
		{
			var values= await _productRepository.GetFilterProductAsync(CategoryId, SubCategoryId);
			return View(values);
		}
	}
}
