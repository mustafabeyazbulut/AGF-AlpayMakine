using AlpayMakina.Dtos.SocialMediaDtos;
using AlpayMakina.Models.DapperContect;
using Dapper;

namespace AlpayMakina.Repositories.SocialMediaRepositories
{
	public class SocialMediaRepository : ISocialMediaRepository
	{
		private readonly Context _context;

		public SocialMediaRepository(Context context)
		{
			_context = context;
		}

        public async Task<List<ResultSocialMediaDto>> GetAllSocialMediaAsync()
		{
			string query = "Select * From Social_Media";
			using (var connection = _context.CreateConnection())
			{
				var values= await connection.QueryAsync<ResultSocialMediaDto>(query);
				return values.ToList();
			}
		}
        public async Task AddSocialMediaAsync(CreateSocialMediaDto createSocialMediaDto)
        {
            string query = "insert into Social_Media (IconClass,Href) values (@IconClass,@Href)";
            var parameters = new DynamicParameters();
            parameters.Add("@IconClass", createSocialMediaDto.IconClass);
            parameters.Add("@Href", createSocialMediaDto.Href);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task RemoveSocialMediaAsync(int id)
        {
            string query = "Delete from Social_Media where Id=@Id";
            var parameters= new DynamicParameters();
            parameters.Add("@Id", id);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<UpdateSocialMediaDto> GetSocialMediaAsync(int id)
        {
            string query = "Select * From Social_Media where Id=@Id";
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryFirstOrDefaultAsync<UpdateSocialMediaDto>(query,parameters);
                return values;
            }
        }

        public async Task UpdateSocialMediaAsync(UpdateSocialMediaDto updateSocialMediaDto)
        {
            string query = "Update Social_Media Set IconClass=@IconClass, Href=@Href where Id=@Id";
            var parameters = new DynamicParameters();
            parameters.Add("@Id", updateSocialMediaDto.Id);
            parameters.Add("@IconClass", updateSocialMediaDto.IconClass);
            parameters.Add("@Href", updateSocialMediaDto.Href);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
