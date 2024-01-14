using AlpayMakina.Repositories.ProductRepositories;
using Microsoft.AspNetCore.Mvc;

namespace AlpayMakina.Areas.UI.ViewComponents
{
    public class _Recommended_UI_ComponentPartial:ViewComponent
    {
        private readonly IProductRepository _productRepository;

        public _Recommended_UI_ComponentPartial(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync(int CategoryId)
        {
            var values=await _productRepository.GetFilterProductAsync(CategoryId);
            return View(values);
        }
    }
}
