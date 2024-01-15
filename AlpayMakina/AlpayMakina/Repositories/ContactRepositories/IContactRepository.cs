using AlpayMakina.Dtos.ContactDtos;

namespace AlpayMakina.Repositories.ContactRepositories
{
    public interface IContactRepository
    {
        Task<List<ResultContactDto>> GetAllContactAsync();
        Task AddContactAsync(CreateContactDto createContactDto);
        Task RemoveContactAsync(int id);
        Task<UpdateContactDto> GetContactAsync(int id);
        Task UpdateContactAsync(UpdateContactDto updateContactDto);
    }
}
