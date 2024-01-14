using AlpayMakina.Dtos.SubCategoryDtos;
using AlpayMakina.Models.DapperContect;
using Dapper;

namespace AlpayMakina.Repositories.SubCategoryRepositories
{
    public class SubCategoryRepository : ISubCategoryRepository
    {
        private readonly Context _context;

        public SubCategoryRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<ResultSubCategoryDto>> GetAllSubCategoryAsync()
        {
            string query = "Select * from SubCategory";
            using(var connection= _context.CreateConnection())
            {
                var value= await connection.QueryAsync<ResultSubCategoryDto>(query);
                return value.ToList();
            }
        }
        public async Task AddSubCategoryAsync(CreateSubCategoryDto createSubCategoryDto)
        {
            string query = "insert into SubCategory (SubCategory,CategoryId) values (@SubCategory,@CategoryId)";
            var parameters = new DynamicParameters();
            parameters.Add("@SubCategory", createSubCategoryDto.SubCategory);
            parameters.Add("@CategoryId", createSubCategoryDto.CategoryId);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task RemoveSubCategoryAsync(int id)
        {
            string query = "Delete from SubCategory where Id=@Id";
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<UpdateSubCategoryDto> GetSubCategoryAsync(int id)
        {
            string query = "Select * From SubCategory where Id=@Id";
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryFirstOrDefaultAsync<UpdateSubCategoryDto>(query, parameters);
                return values;
            }
        }

        public async Task UpdateSubCategoryAsync(UpdateSubCategoryDto updateSubCategoryDto)
        {
            string query = "Update SubCategory Set SubCategory=@SubCategory, CategoryId=@CategoryId where Id=@Id";
            var parameters = new DynamicParameters();
            parameters.Add("@Id", updateSubCategoryDto.Id);
            parameters.Add("@SubCategory", updateSubCategoryDto.SubCategory);
            parameters.Add("@CategoryId", updateSubCategoryDto.CategoryId);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
