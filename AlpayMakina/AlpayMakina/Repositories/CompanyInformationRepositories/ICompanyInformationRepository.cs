using AlpayMakina.Dtos.CompanyInformationDtos;

namespace AlpayMakina.Repositories.CompanyInformationRepositories
{
    public interface ICompanyInformationRepository
    {
        Task<List<ResultCompanyInformationDto>> GetAllCompanyInformationAsync();
        Task<ResultCompanyInformationDto> GetLastCompanyInformationAsync();
        Task AddCompanyInformationAsync(CreateCompanyInformationDto createCompanyInformationDto);
        Task RemoveCompanyInformationAsync(int id);
        Task<UpdateCompanyInformationDto> GetCompanyInformationAsync(int id);
        Task UpdateCompanyInformationAsync(UpdateCompanyInformationDto updateCompanyInformationDto);
    }
}
