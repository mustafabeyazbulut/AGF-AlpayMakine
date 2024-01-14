using AlpayMakina.Dtos.ProductDetailDtos;
using AlpayMakina.Dtos.ProductDtos;

namespace AlpayMakina.Repositories.ProductRepositories
{
    public interface IProductRepository
    {
        Task<List<ResultProductDto>> GetAllProductAsync();
        Task<List<ResultProductDto>> GetFilterProductAsync(int categoryId = 0, int subCategoryId = 0);
        Task AddProductAsync(CreateProductDto createProductDto);
        Task RemoveProductAsync(int id);
        Task<UpdateProductDto> GetProductAsync(int id);
        Task<ResultProductDetailDto> GetProductDetailAsync(int id);
        Task UpdateProductAsync(UpdateProductDto updateProductDto);
    }
}
