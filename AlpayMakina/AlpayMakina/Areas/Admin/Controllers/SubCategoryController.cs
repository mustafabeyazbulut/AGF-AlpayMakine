using AlpayMakina.Dtos.SubCategoryDtos;
using AlpayMakina.Repositories.CategoryRepositories;
using AlpayMakina.Repositories.SubCategoryRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AlpayMakina.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/SubCategory")]
    public class SubCategoryController : Controller
    {
        private readonly ISubCategoryRepository _SubCategoryRepository;
        private readonly ICategoryRepository _categoryRepository;

        public SubCategoryController(ISubCategoryRepository SubCategoryRepository, ICategoryRepository categoryRepository)
        {
            _SubCategoryRepository = SubCategoryRepository;
            _categoryRepository = categoryRepository;
        }
        [Route("")]
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var values = await _SubCategoryRepository.GetAllSubCategoryAsync();
            return View(values);
        }

        [Route("CreateSubCategory")]
        public async Task<IActionResult> CreateSubCategory()
        {
            var values= await _categoryRepository.GetAllCategoryAsync();
            List<SelectListItem> category = (from x in values
                                                select new SelectListItem
                                                {
                                                    Text = x.Category,
                                                    Value = x.Id.ToString()
                                                }).ToList();
            ViewBag.Category = category;
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

            var values = await _categoryRepository.GetAllCategoryAsync();
            List<SelectListItem> category = (from x in values
                                             select new SelectListItem
                                             {
                                                 Text = x.Category,
                                                 Value = x.Id.ToString()
                                             }).ToList();
            ViewBag.Category = category;

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
