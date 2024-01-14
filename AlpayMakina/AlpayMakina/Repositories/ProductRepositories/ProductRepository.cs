using AlpayMakina.Dtos.ProductDetailDtos;
using AlpayMakina.Dtos.ProductDtos;
using AlpayMakina.Models.DapperContect;
using Dapper;

namespace AlpayMakina.Repositories.ProductRepositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly Context _context;

        public ProductRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<ResultProductDto>> GetAllProductAsync()
        {
            string query = @"
                            Select 
                                Product.Id,
                                Product.Title,
                                Product.Price,                
                                Product.Currency,                
                                Product.ImageUrl,                
                                Category.Category,                
                                Product.CategoryId,                
                                SubCategory.SubCategory,                
                                Product.SubCategoryId                
                             FROM 
                                Product
                             LEFT JOIN
                                Category On Product.CategoryId=Category.Id
                             LEFT JOIN
                                SubCategory On Product.SubCategoryId=SubCategory.Id";
            using (var connection=_context.CreateConnection())
            {
                var values= await connection.QueryAsync<ResultProductDto>(query);
                return values.ToList();
            }

        }
        public async Task<List<ResultProductDto>> GetFilterProductAsync(int categoryId = 0, int subCategoryId = 0)
        {
            string query = "Select * from Product";
            using (var connection = _context.CreateConnection())
            {
                var products = await connection.QueryAsync<ResultProductDto>(query);

                // Filtreleme işlemleri
                if (subCategoryId != 0)
                {
                    // Eğer subCategoryId varsa, sadece o subCategory'e ait ürünleri getir
                    products = products.Where(p => p.SubCategoryId == subCategoryId);
                }
                else if (categoryId != 0)
                {
                    // Eğer sadece categoryId varsa, sadece o category'e ait ürünleri getir
                    products = products.Where(p => p.CategoryId == categoryId);
                }

                // Filtreleme sonucunu liste olarak döndür
                return products.ToList();
            }
        }
        public async Task AddProductAsync(CreateProductDto createProductDto)
        {
            string query = "insert into Product (Title,Price,Currency,ImageUrl,CategoryId,SubCategoryId) values (@Title,@Price,@Currency,@ImageUrl,@CategoryId,@SubCategoryId)";
            var parameters = new DynamicParameters();
            parameters.Add("@Title", createProductDto.Title);
            parameters.Add("@Price", createProductDto.Price);
            parameters.Add("@Currency", createProductDto.Currency);
            parameters.Add("@ImageUrl", createProductDto.ImageUrl);
            parameters.Add("@CategoryId", createProductDto.CategoryId);
            parameters.Add("@SubCategoryId", createProductDto.SubCategoryId);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task RemoveProductAsync(int id)
        {
            string query = "Delete from Product where Id=@Id";
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<UpdateProductDto> GetProductAsync(int id)
        {
            string query = "Select * From Product where Id=@Id";
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryFirstOrDefaultAsync<UpdateProductDto>(query, parameters);
                return values;
            }
        }

        public async Task UpdateProductAsync(UpdateProductDto updateProductDto)
        {
            string query = "Update Product Set Title=@Title, Price=@Price, Currency=@Currency, ImageUrl=@ImageUrl, CategoryId=@CategoryId, SubCategoryId=@SubCategoryId where Id=@Id";
            var parameters = new DynamicParameters();
            parameters.Add("@Id", updateProductDto.Id);
            parameters.Add("@Title", updateProductDto.Title);
            parameters.Add("@Price", updateProductDto.Price);
            parameters.Add("@Currency", updateProductDto.Currency);
            parameters.Add("@ImageUrl", updateProductDto.ImageUrl);
            parameters.Add("@CategoryId", updateProductDto.CategoryId);
            parameters.Add("@SubCategoryId", updateProductDto.SubCategoryId);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

		public async Task<ResultProductDetailDto> GetProductDetailAsync(int id)
		{
			string query = @"
                            Select 
                                Product.Id,
                                Product.Title,
                                Product.Price,                
                                Product.Currency,                
                                Product.ImageUrl,                
                                Category.Category,                
                                Product.CategoryId,                
                                SubCategory.SubCategory,                
                                Product.SubCategoryId                
                             FROM 
                                Product
                             LEFT JOIN
                                Category On Product.CategoryId=Category.Id
                             LEFT JOIN
                                SubCategory On Product.SubCategoryId=SubCategory.Id
                             WHERE 
                             Product.Id=@Id";
			var parameters = new DynamicParameters();
			parameters.Add("@Id", id);
			using (var connection = _context.CreateConnection())
			{
				var values = await connection.QueryFirstOrDefaultAsync<ResultProductDetailDto>(query, parameters);
				return values;
			}
		}
	}
}
