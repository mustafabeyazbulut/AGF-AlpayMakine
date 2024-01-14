using AlpayMakina.Dtos.CategoryDtos;
using AlpayMakina.Repositories.CategoryRepositories;
using Microsoft.AspNetCore.Mvc;

namespace AlpayMakina.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Category")]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _CategoryRepository;

        public CategoryController(ICategoryRepository CategoryRepository)
        {
            _CategoryRepository = CategoryRepository;
        }
        [Route("")]
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var values = await _CategoryRepository.GetAllCategoryAsync();
            return View(values);
        }

        [Route("CreateCategory")]
        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        [Route("CreateCategory")]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto createCategoryDto)
        {
            await _CategoryRepository.AddCategoryAsync(createCategoryDto);

            return RedirectToAction("Index", "Category", new { area = "Admin" });
        }
        [Route("RemoveCategory/{id}")]
        public async Task<IActionResult> RemoveCategory(int id)
        {
            await _CategoryRepository.RemoveCategoryAsync(id);

            return RedirectToAction("Index", "Category", new { area = "Admin" });
        }

        [HttpGet]
        [Route("UpdateCategory/{id}")]
        public async Task<IActionResult> UpdateCategory(int id)
        {
            var value = await _CategoryRepository.GetCategoryAsync(id);

            return View(value);
        }

        [HttpPost]
        [Route("UpdateCategory/{id}")]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto updateCategoryDto)
        {
            await _CategoryRepository.UpdateCategoryAsync(updateCategoryDto);

            return RedirectToAction("Index", "Category", new { area = "Admin" });

        }
    }
}
