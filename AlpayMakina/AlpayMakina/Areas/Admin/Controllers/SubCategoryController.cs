using AlpayMakina.Dtos.SubCategoryDtos;
using AlpayMakina.Repositories.SubCategoryRepositories;
using Microsoft.AspNetCore.Mvc;

namespace AlpayMakina.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/SubCategory")]
    public class SubCategoryController : Controller
    {
        private readonly ISubCategoryRepository _SubCategoryRepository;

        public SubCategoryController(ISubCategoryRepository SubCategoryRepository)
        {
            _SubCategoryRepository = SubCategoryRepository;
        }
        [Route("")]
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var values = await _SubCategoryRepository.GetAllSubCategoryAsync();
            return View(values);
        }

        [Route("CreateSubCategory")]
        public IActionResult CreateSubCategory()
        {
            return View();
        }

        [HttpPost]
        [Route("CreateSubCategory")]
        public async Task<IActionResult> CreateSubCategory(CreateSubCategoryDto createSubCategoryDto)
        {
            await _SubCategoryRepository.AddSubCategoryAsync(createSubCategoryDto);

            return RedirectToAction("Index", "SubCategory", new { area = "Admin" });
        }
        [Route("RemoveSubCategory/{id}")]
        public async Task<IActionResult> RemoveSubCategory(int id)
        {
            await _SubCategoryRepository.RemoveSubCategoryAsync(id);

            return RedirectToAction("Index", "SubCategory", new { area = "Admin" });
        }

        [HttpGet]
        [Route("UpdateSubCategory/{id}")]
        public async Task<IActionResult> UpdateSubCategory(int id)
        {
            var value = await _SubCategoryRepository.GetSubCategoryAsync(id);

            return View(value);
        }

        [HttpPost]
        [Route("UpdateSubCategory/{id}")]
        public async Task<IActionResult> UpdateSubCategory(UpdateSubCategoryDto updateSubCategoryDto)
        {
            await _SubCategoryRepository.UpdateSubCategoryAsync(updateSubCategoryDto);

            return RedirectToAction("Index", "SubCategory", new { area = "Admin" });

        }
    }
}
