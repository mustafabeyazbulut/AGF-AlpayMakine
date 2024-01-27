using AlpayMakina.Dtos.SubCategoryDtos;
using AlpayMakina.Repositories.ProductRepositories;
using Microsoft.AspNetCore.Mvc;

namespace AlpayMakina.Areas.UI.Controllers
{
    [Area("UI")]
    [Route("UI/Products")]
    public class ProductsController : Controller
    {
        private readonly IProductRepository _productRepository;

		public ProductsController(IProductRepository productRepository)
		{
			_productRepository = productRepository;
		}

		[HttpGet]
        [Route("")]
        [Route("Index")]
        public  IActionResult Index()
        {
            ViewBag.CategoryId = 0;
            ViewBag.SubCategoryId = 0;
            return View();
        }

        [Route("{CategoryId}")]
        [Route("Index/{CategoryId}")]
        [Route("{CategoryId}/{SubCategoryId}")]
        [Route("Index/{CategoryId}/{SubCategoryId}")]
        public IActionResult Index(int CategoryId=0,int SubCategoryId=0 )
        {
            ViewBag.CategoryId = CategoryId;
            ViewBag.SubCategoryId = SubCategoryId;
            return View();
        }
    }
}
