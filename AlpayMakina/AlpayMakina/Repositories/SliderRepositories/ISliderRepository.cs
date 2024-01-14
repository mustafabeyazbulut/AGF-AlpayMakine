using AlpayMakina.Dtos.SliderDtos;

namespace AlpayMakina.Repositories.SliderRepositories
{
    public interface ISliderRepository
    {
        Task<List<ResultSliderDto>> GetAllSliderAsync();
        Task AddSliderAsync(CreateSliderDto createSliderDto);
        Task RemoveSliderAsync(int id);
        Task<UpdateSliderDto> GetSliderAsync(int id);
        Task UpdateSliderAsync(UpdateSliderDto updateSliderDto);
    }
}
