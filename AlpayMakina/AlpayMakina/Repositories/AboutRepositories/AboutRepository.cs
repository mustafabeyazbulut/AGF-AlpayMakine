using AlpayMakina.Dtos.AboutDtos;
using AlpayMakina.Models.DapperContect;
using Dapper;

namespace AlpayMakina.Repositories.AboutRepositories
{
    public class AboutRepository:IAboutRepository
    {
        private readonly Context _context;

        public AboutRepository(Context context)
        {
            _context = context;
        }
        public async Task<List<ResultAboutDto>> GetAllAboutAsync()
        {
            var query = "Select * from About";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultAboutDto>(query);
                return values.ToList();
            }
        }
        public async Task AddAboutAsync(CreateAboutDto createAboutDto)
        {
            string query = "insert into About (Title,HTime,HDate,ImageUrl,Description) values (@Title,@HTime,@HDate,@ImageUrl,@Description)";
            var parameters = new DynamicParameters();
            parameters.Add("@Title", createAboutDto.Title);
            parameters.Add("@HTime", createAboutDto.HTime);
            parameters.Add("@HDate", createAboutDto.HDate);
            parameters.Add("@ImageUrl", createAboutDto.ImageUrl);
            parameters.Add("@Description", createAboutDto.Description);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
        public async Task RemoveAboutAsync(int id)
        {
            string query = "Delete from About where Id=@Id";
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
        public async Task<UpdateAboutDto> GetAboutAsync(int id)
        {
            string query = "Select * From About where Id=@Id";
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryFirstOrDefaultAsync<UpdateAboutDto>(query, parameters);
                return values;
            }
        }
        public async Task UpdateAboutAsync(UpdateAboutDto updateAboutDto)
        {
            string query = "Update About Set Title=@Title,HTime=@HTime,HDate=@HDate,ImageUrl=@ImageUrl,Description=@Description where Id=@Id";
            var parameters = new DynamicParameters();
            parameters.Add("@Id", updateAboutDto.Id);
            parameters.Add("@Title", updateAboutDto.Title);
            parameters.Add("@HTime", updateAboutDto.HTime);
            parameters.Add("@HDate", updateAboutDto.HDate);
            parameters.Add("@ImageUrl", updateAboutDto.ImageUrl);
            parameters.Add("@Description", updateAboutDto.Description);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
