using AlpayMakina.Dtos.SubCategoryDtos;

namespace AlpayMakina.Repositories.SubCategoryRepositories
{
    public interface ISubCategoryRepository
    {
        Task<List<ResultSubCategoryDto>> GetAllSubCategoryAsync();
        Task AddSubCategoryAsync(CreateSubCategoryDto createSubCategoryDto);
        Task RemoveSubCategoryAsync(int id);
        Task<UpdateSubCategoryDto> GetSubCategoryAsync(int id);
        Task UpdateSubCategoryAsync(UpdateSubCategoryDto updateSubCategoryDto);
    }
}
