using AlpayMakina.Repositories.ProductRepositories;
using Microsoft.AspNetCore.Mvc;

namespace AlpayMakina.Areas.UI.Controllers
{
    [Area("UI")]
    [Route("UI/ProductDetail")]
    public class ProductDetailController : Controller
    {
        private readonly IProductRepository _productRepository;

		public ProductDetailController(IProductRepository productRepository)
		{
			_productRepository = productRepository;
		}

		[HttpGet]
        [Route("{id}")]
		[Route("Index/{id}")]
		public async Task<IActionResult> Index(int id)
        {
            var values= await _productRepository.GetProductDetailAsync(id);
            return View(values);
        }
    }
}
