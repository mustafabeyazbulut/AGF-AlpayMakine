using AlpayMakina.Dtos.SliderDtos;
using AlpayMakina.Models.DapperContect;
using Dapper;

namespace AlpayMakina.Repositories.SliderRepositories
{
    public class SliderRepository : ISliderRepository
    {
        private readonly Context _context;

        public SliderRepository(Context context)
        {
            _context = context;
        }
        public async Task<List<ResultSliderDto>> GetAllSliderAsync()
        {
            string query = "Select * From Slider";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultSliderDto>(query);
                return values.ToList();
            }
        }
        public async Task AddSliderAsync(CreateSliderDto createSliderDto)
        {
            string query = "insert into Slider (Title,Description,ImageUrl,PriceUrl,Active) values (@Title,@Description,@ImageUrl,@PriceUrl,@Active)";
            var parameters = new DynamicParameters();
            parameters.Add("@Title", createSliderDto.Title);
            parameters.Add("@Description", createSliderDto.Description);
            parameters.Add("@ImageUrl", createSliderDto.ImageUrl);
            parameters.Add("@PriceUrl", createSliderDto.PriceUrl);
            parameters.Add("@Active", createSliderDto.Active);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task RemoveSliderAsync(int id)
        {
            string query = "Delete from Slider where Id=@Id";
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<UpdateSliderDto> GetSliderAsync(int id)
        {
            string query = "Select * From Slider where Id=@Id";
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryFirstOrDefaultAsync<UpdateSliderDto>(query, parameters);
                return values;
            }
        }

        public async Task UpdateSliderAsync(UpdateSliderDto updateSliderDto)
        {
            string query = "Update Slider Set Title=@Title, Description=@Description, ImageUrl=@ImageUrl, PriceUrl=@PriceUrl, Active=@Active where Id=@Id";
            var parameters = new DynamicParameters();
            parameters.Add("@Id", updateSliderDto.Id);
            parameters.Add("@Title", updateSliderDto.Title);
            parameters.Add("@Description", updateSliderDto.Description);
            parameters.Add("@ImageUrl", updateSliderDto.ImageUrl);
            parameters.Add("@PriceUrl", updateSliderDto.PriceUrl);
            parameters.Add("@Active", updateSliderDto.Active);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
