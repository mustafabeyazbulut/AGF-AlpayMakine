using AlpayMakina.Dtos.ProductDtos;
using AlpayMakina.Repositories.CategoryRepositories;
using AlpayMakina.Repositories.ProductRepositories;
using AlpayMakina.Repositories.SubCategoryRepositories;
using AlpayMakina.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AlpayMakina.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Product")]
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductRepository _ProductRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISubCategoryRepository _subCategoryRepository;
        private readonly ImageOperations _imageOperations;

        public ProductController(IProductRepository ProductRepository,
            ICategoryRepository categoryRepository,
            ISubCategoryRepository subCategoryRepository,
            ImageOperations imageOperations)
        {
            _ProductRepository = ProductRepository;
            _categoryRepository = categoryRepository;
            _subCategoryRepository = subCategoryRepository;
            _imageOperations = imageOperations;
        }
        [Route("")]
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var values = await _ProductRepository.GetAllProductAsync();
            return View(values);
        }

        [Route("CreateProduct")]
        public async Task<IActionResult> CreateProduct()
        {
            var valuesCategory = await _categoryRepository.GetAllCategoryAsync();
            List<SelectListItem> category = (from x in valuesCategory
                                             select new SelectListItem
                                             {
                                                 Text = x.Category,
                                                 Value = x.Id.ToString()
                                             }).ToList();
            category.Insert(0, new SelectListItem
            {
                Text = "Kategori Seçebilirsiniz",
                Value = "0"
            });
            ViewBag.Category = category;

            var valuesSub = await _subCategoryRepository.GetAllSubCategoryAsync();
            List<SelectListItem> subCategory = (from x in valuesSub
                                                select new SelectListItem
                                                {
                                                    Text = x.SubCategory,
                                                    Value = x.Id.ToString()
                                                }).ToList();
            subCategory.Insert(0, new SelectListItem
            {
                Text = "Alt Kategori Seçebilirsiniz",
                Value = "0"
            });
            ViewBag.SubCategory = subCategory;

            return View();
        }

        [HttpPost]
        [Route("CreateProduct")]
        public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto)
        {
            if (createProductDto.ImageFile != null && createProductDto.ImageFile.Length > 0)
            {
                createProductDto.ImageUrl = await _imageOperations.UploadImageAsync(createProductDto.ImageFile);
            }

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
            var valuesCategory = await _categoryRepository.GetAllCategoryAsync();
            List<SelectListItem> category = (from x in valuesCategory
                                             select new SelectListItem
                                             {
                                                 Text = x.Category,
                                                 Value = x.Id.ToString()
                                             }).ToList();
            ViewBag.Category = category;

            var valuesSub = await _subCategoryRepository.GetAllSubCategoryAsync();
            List<SelectListItem> subCategory = (from x in valuesSub
                                                select new SelectListItem
                                                {
                                                    Text = x.SubCategory,
                                                    Value = x.Id.ToString()
                                                }).ToList();
            ViewBag.SubCategory = subCategory;


            var value = await _ProductRepository.GetProductAsync(id);

            return View(value);
        }

        [HttpPost]
        [Route("UpdateProduct/{id}")]
        public async Task<IActionResult> UpdateProduct(UpdateProductDto updateProductDto)
        {
            if (updateProductDto.ImageFile != null && updateProductDto.ImageFile.Length > 0)
            {
                var lastProduct = await _ProductRepository.GetProductAsync(updateProductDto.Id);

                if (await _imageOperations.DeleteIconAsync(lastProduct.ImageUrl) || string.IsNullOrEmpty(lastProduct.ImageUrl))
                {
                    updateProductDto.ImageUrl = await _imageOperations.UploadImageAsync(updateProductDto.ImageFile);
                }
            }
            await _ProductRepository.UpdateProductAsync(updateProductDto);

            return RedirectToAction("Index", "Product", new { area = "Admin" });

        }
    }
}
