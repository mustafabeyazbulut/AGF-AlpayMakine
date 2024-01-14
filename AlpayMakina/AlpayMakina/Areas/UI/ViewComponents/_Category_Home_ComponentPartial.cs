using AlpayMakina.Repositories.CategoryRepositories;
using Microsoft.AspNetCore.Mvc;

namespace AlpayMakina.Areas.UI.ViewComponents
{
	public class _Category_Home_ComponentPartial:ViewComponent
	{
		private readonly ICategoryRepository _categoryRepository;

        public _Category_Home_ComponentPartial(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
		{
			var values= await _categoryRepository.GetAllCategoryWithSubCategoryAsync();
			return View(values);
		}
	}
}
