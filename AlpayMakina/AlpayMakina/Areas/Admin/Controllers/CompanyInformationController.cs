using AlpayMakina.Dtos.CompanyInformationDtos;
using AlpayMakina.Dtos.SocialMediaDtos;
using AlpayMakina.Repositories.CompanyInformationRepositories;
using Microsoft.AspNetCore.Mvc;

namespace AlpayMakina.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/CompanyInformation")]
    public class CompanyInformationController : Controller
    {
        private readonly ICompanyInformationRepository _companyInformationRepository;

        public CompanyInformationController(ICompanyInformationRepository companyInformationRepository)
        {
            _companyInformationRepository = companyInformationRepository;
        }

        [Route("")]
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var values= await _companyInformationRepository.GetAllCompanyInformationAsync();
            return View(values);
        }
        [Route("CreateCompanyInformation")]
        public IActionResult CreateCompanyInformation()
        {
            return View();
        }
        [HttpPost]
        [Route("CreateCompanyInformation")]
        public async Task<IActionResult> CreateCompanyInformation(CreateCompanyInformationDto createCompanyInformationDto)
        {
            await _companyInformationRepository.AddCompanyInformationAsync(createCompanyInformationDto);
            return RedirectToAction("Index", "CompanyInformation", new { area = "Admin" });
        }
        [Route("RemoveCompanyInformation/{id}")]
        public async Task<IActionResult> RemoveCompanyInformation(int id)
        {
            await _companyInformationRepository.RemoveCompanyInformationAsync(id);
            return RedirectToAction("Index", "CompanyInformation", new { area = "Admin" });
        }
        [HttpGet]
        [Route("UpdateCompanyInformation/{id}")]
        public async Task<IActionResult> UpdateCompanyInformation(int id)
        {
            var value= await _companyInformationRepository.GetCompanyInformationAsync(id);
            return View(value);
        }
        [HttpPost]
        [Route("UpdateCompanyInformation/{id}")]
        public async Task<IActionResult> UpdateCompanyInformation(UpdateCompanyInformationDto updateCompanyInformationDto)
        {
            await _companyInformationRepository.UpdateCompanyInformationAsync(updateCompanyInformationDto);
            return RedirectToAction("Index", "CompanyInformation", new { area = "Admin" });

        }
    }
}
