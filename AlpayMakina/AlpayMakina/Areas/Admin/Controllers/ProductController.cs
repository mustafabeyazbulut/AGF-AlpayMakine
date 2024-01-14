using AlpayMakina.Dtos.ProductDtos;
using AlpayMakina.Repositories.ProductRepositories;
using Microsoft.AspNetCore.Mvc;

namespace AlpayMakina.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Product")]
    public class ProductController : Controller
    {
        private readonly IProductRepository _ProductRepository;

        public ProductController(IProductRepository ProductRepository)
        {
            _ProductRepository = ProductRepository;
        }
        [Route("")]
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var values = await _ProductRepository.GetAllProductAsync();
            return View(values);
        }

        [Route("CreateProduct")]
        public IActionResult CreateProduct()
        {
            return View();
        }

        [HttpPost]
        [Route("CreateProduct")]
        public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto)
        {
            await _ProductRepository.AddProductAsync(createProductDto);

            return RedirectToAction("Index", "Product", new { area = "Admin" });
        }
        [Route("RemoveProduct/{id}")]
        public async Task<IActionResult> RemoveProduct(int id)
        {
            await _ProductRepository.RemoveProductAsync(id);

            return RedirectToAction("Index", "Product", new { area = "Admin" });
        }

        [HttpGet]
        [Route("UpdateProduct/{id}")]
        public async Task<IActionResult> UpdateProduct(int id)
        {
            var value = await _ProductRepository.GetProductAsync(id);

            return View(value);
        }

        [HttpPost]
        [Route("UpdateProduct/{id}")]
        public async Task<IActionResult> UpdateProduct(UpdateProductDto updateProductDto)
        {
            await _ProductRepository.UpdateProductAsync(updateProductDto);

            return RedirectToAction("Index", "Product", new { area = "Admin" });

        }
    }
}
