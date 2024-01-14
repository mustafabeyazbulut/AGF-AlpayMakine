using AlpayMakina.Dtos.UserDtos;

namespace AlpayMakina.Repositories.UserRepositories
{
    public interface IUserRepository
    {
        Task<List<ResultUserDto>> GetAllUserAsync();
        Task AddUserAsync(CreateUserDto userDto);
        Task RemoveUserAsync(int id);
        Task<UpdateUserDto> GetUserWithUpdateAsync(int id);
        Task UpdateUserAsync(UpdateUserDto userDto);
        Task<ResultUserDto> GetUserWithEmail(string email);
    }
}
