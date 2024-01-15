using AlpayMakina.Dtos.AboutDtos;

namespace AlpayMakina.Repositories.AboutRepositories
{
    public interface IAboutRepository
    {
        Task<List<ResultAboutDto>> GetAllAboutAsync();
        Task AddAboutAsync(CreateAboutDto createAboutDto);
        Task RemoveAboutAsync(int id);
        Task<UpdateAboutDto> GetAboutAsync(int id);
        Task UpdateAboutAsync(UpdateAboutDto updateAboutDto);
    }
}
