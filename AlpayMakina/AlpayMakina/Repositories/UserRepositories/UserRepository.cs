using AlpayMakina.Dtos.UserDtos;
using AlpayMakina.Models.DapperContect;
using AlpayMakina.Utils;
using Dapper;

namespace AlpayMakina.Repositories.UserRepositories
{
    
    public class UserRepository : IUserRepository
    {
        private readonly Context _context;
        private readonly HashHelper _hashHelper;

        public UserRepository(Context context, HashHelper hashHelper)
        {
            _context = context;
            _hashHelper = hashHelper;
        }
        public async Task<List<ResultUserDto>> GetAllUserAsync()
        {
            var query = "Select * from tblUser";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultUserDto>(query);
                return values.ToList();
            }
        }
        public async Task AddUserAsync(CreateUserDto userDto)
        {
            userDto.Password = _hashHelper.HashPassword(userDto.Password);

            string query = "insert into tblUser (Email,Password,FullName) values (@Email,@Password,@FullName)";
            var parameters = new DynamicParameters();
            parameters.Add("@Email", userDto.Email);
            parameters.Add("@Password", userDto.Password);
            parameters.Add("@FullName", userDto.FullName);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task RemoveUserAsync(int id)
        {
            string query = "Delete from tblUser where Id=@Id";
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<UpdateUserDto> GetUserWithUpdateAsync(int id)
        {
            string query = "Select * From tblUser where Id=@Id";
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryFirstOrDefaultAsync<UpdateUserDto>(query, parameters);
                return values;
            }
        }

        public async Task UpdateUserAsync(UpdateUserDto userDto)
        {
            var lastUser = GetUserWithUpdateAsync(userDto.Id).Result;

            if (lastUser != null && lastUser.Password!= userDto.Password)
            {
                userDto.Password = _hashHelper.HashPassword(userDto.Password);
            }
 
            string query = "Update tblUser Set Email=@Email,Password=@Password,FullName=@FullName where Id=@Id";
            var parameters = new DynamicParameters();
            parameters.Add("@Id", userDto.Id);
            parameters.Add("@Email", userDto.Email);
            parameters.Add("@Password", userDto.Password);
            parameters.Add("@FullName", userDto.FullName);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<ResultUserDto> GetUserWithEmail(string email)
        {
            string query = "Select * From tblUser where Email=@Email";
            var parameters = new DynamicParameters();
            parameters.Add("@Email", email);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryFirstOrDefaultAsync<ResultUserDto>(query, parameters);
                return values;
            }
        }
    }
}
