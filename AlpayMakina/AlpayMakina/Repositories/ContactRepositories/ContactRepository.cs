using AlpayMakina.Dtos.ContactDtos;
using AlpayMakina.Models.DapperContect;
using Dapper;

namespace AlpayMakina.Repositories.ContactRepositories
{
    public class ContactRepository:IContactRepository
    {
        private readonly Context _context;

        public ContactRepository(Context context)
        {
            _context = context;
        }
        public async Task<List<ResultContactDto>> GetAllContactAsync()
        {
            var query = "Select * from Contact";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultContactDto>(query);
                return values.ToList();
            }
        }
        public async Task AddContactAsync(CreateContactDto createContactDto)
        {
            string query = "insert into Contact (CompanyName,CompanyAddress,Mobile,Fax,Email) values (@CompanyName,@CompanyAddress,@Mobile,@Fax,@Email)";
            var parameters = new DynamicParameters();
            parameters.Add("@CompanyName", createContactDto.CompanyName);
            parameters.Add("@CompanyAddress", createContactDto.CompanyAddress);
            parameters.Add("@Mobile", createContactDto.Mobile);
            parameters.Add("@Fax", createContactDto.Fax);
            parameters.Add("@Email", createContactDto.Email);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
        public async Task RemoveContactAsync(int id)
        {
            string query = "Delete from Contact where Id=@Id";
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
        public async Task<UpdateContactDto> GetContactAsync(int id)
        {
            string query = "Select * From Contact where Id=@Id";
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryFirstOrDefaultAsync<UpdateContactDto>(query, parameters);
                return values;
            }
        }
        public async Task UpdateContactAsync(UpdateContactDto updateContactDto)
        {
            string query = "Update Contact Set CompanyName=@CompanyName,CompanyAddress=@CompanyAddress,Mobile=@Mobile,Fax=@Fax,Email=@Email where Id=@Id";
            var parameters = new DynamicParameters();
            parameters.Add("@Id", updateContactDto.Id);
            parameters.Add("@CompanyName", updateContactDto.CompanyName);
            parameters.Add("@CompanyAddress", updateContactDto.CompanyAddress);
            parameters.Add("@Mobile", updateContactDto.Mobile);
            parameters.Add("@Fax", updateContactDto.Fax);
            parameters.Add("@Email", updateContactDto.Email);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
