using AlpayMakina.Repositories.CompanyInformationRepositories;
using Microsoft.AspNetCore.Mvc;

namespace AlpayMakina.ViewComponents.UILayoutViewComponents
{
    public class _Footer_UILayout_ComponentPartial:ViewComponent
    {
        private readonly ICompanyInformationRepository _repository;

        public _Footer_UILayout_ComponentPartial(ICompanyInformationRepository repository)
        {
            _repository = repository;
        }

        public async Task< IViewComponentResult> InvokeAsync()
        {
            var values= await _repository.GetLastCompanyInformationAsync();
            return View(values);
        }
    }
}
