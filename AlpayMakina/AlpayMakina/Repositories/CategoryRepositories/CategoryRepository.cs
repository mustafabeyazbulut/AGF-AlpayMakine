using AlpayMakina.Dtos.CategoryDtos;
using AlpayMakina.Dtos.SubCategoryDtos;
using AlpayMakina.Models.DapperContect;
using Dapper;

namespace AlpayMakina.Repositories.CategoryRepositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly Context _context;

        public CategoryRepository(Context context)
        {
            _context = context;
        }
        public async Task<List<ResultCategoryDto>> GetAllCategoryAsync()
        {
            var query = "Select * from Category";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultCategoryDto>(query);
                return values.ToList();
            }
        }

        public async Task<List<ResultCategoryWithSubCategory>> GetAllCategoryWithSubCategoryAsync()
        {
            const string query = @"
            SELECT *
            FROM Category c
            LEFT JOIN SubCategory s ON c.Id = s.CategoryId";

            using (var connection = _context.CreateConnection())
            {
                var categoryDictionary = new Dictionary<int, ResultCategoryWithSubCategory>();

                var result = await connection.QueryAsync<ResultCategoryWithSubCategory, SubCategory,
                    ResultCategoryWithSubCategory>(query, (category, subCategory) =>
                    {
                        if (!categoryDictionary.TryGetValue(category.Id, out var currentCategory))
                        {
                            currentCategory = category;
                            currentCategory.SubCategoryList = new List<SubCategory>();
                            categoryDictionary.Add(currentCategory.Id, currentCategory);
                        }

                        if (subCategory != null)
                        {
                            currentCategory.SubCategoryList.Add(subCategory);
                        }

                        return currentCategory;
                    }, splitOn: "Id");

                return result.Distinct().ToList();
            }
        }
        public async Task AddCategoryAsync(CreateCategoryDto createCategoryDto)
        {
            string query = "insert into Category (Category) values (@Category)";
            var parameters = new DynamicParameters();
            parameters.Add("@Category", createCategoryDto.Category);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task RemoveCategoryAsync(int id)
        {
            string query = "Delete from Category where Id=@Id";
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<UpdateCategoryDto> GetCategoryAsync(int id)
        {
            string query = "Select * From Category where Id=@Id";
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryFirstOrDefaultAsync<UpdateCategoryDto>(query, parameters);
                return values;
            }
        }

        public async Task UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto)
        {
            string query = "Update Category Set Category=@Category where Id=@Id";
            var parameters = new DynamicParameters();
            parameters.Add("@Id", updateCategoryDto.Id);
            parameters.Add("@Category", updateCategoryDto.Category);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
