using AlpayMakina.Dtos.CategoryDtos;

namespace AlpayMakina.Repositories.CategoryRepositories
{
    public interface ICategoryRepository
    {
        Task<List<ResultCategoryDto>> GetAllCategoryAsync();
        Task<List<ResultCategoryWithSubCategory>> GetAllCategoryWithSubCategoryAsync();
        Task AddCategoryAsync(CreateCategoryDto createCategoryDto);
        Task RemoveCategoryAsync(int id);
        Task<UpdateCategoryDto> GetCategoryAsync(int id);
        Task UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto);
    }
}
