using AlpayMakina.Dtos.CompanyInformationDtos;
using AlpayMakina.Dtos.SocialMediaDtos;
using AlpayMakina.Models.DapperContect;
using Dapper;

namespace AlpayMakina.Repositories.CompanyInformationRepositories
{
    public class CompanyInformationRepository : ICompanyInformationRepository
    {
        private readonly Context _context;

        public CompanyInformationRepository(Context context)
        {
            _context = context;
        }
        public async Task<List<ResultCompanyInformationDto>> GetAllCompanyInformationAsync()
        {
            string query = "Select * From Company_Information";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultCompanyInformationDto>(query);
                return values.ToList();
            }
        }

        public async Task<ResultCompanyInformationDto> GetLastCompanyInformationAsync()
        {
            string query = "select * from Company_Information Where Id=(Select MAX(Id) from Company_Information)";
            using (var connection = _context.CreateConnection())
            {
                var value = await connection.QueryFirstAsync<ResultCompanyInformationDto>(query);
                return value;
            }
        }
        public async Task<UpdateCompanyInformationDto> GetCompanyInformationAsync(int id)
        {
            string query = "Select * From Company_Information where Id=@Id";
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryFirstOrDefaultAsync<UpdateCompanyInformationDto>(query, parameters);
                return values;
            }
        }
        public async Task AddCompanyInformationAsync(CreateCompanyInformationDto createCompanyInformationDto)
        {
            string query = "insert into Company_Information (Name,Address,Mail,Description,PhoneNumber) values (@Name,@Address,@Mail,@Description,@PhoneNumber)";
            var parameters = new DynamicParameters();
            parameters.Add("@Name", createCompanyInformationDto.Name);
            parameters.Add("@Address", createCompanyInformationDto.Address);
            parameters.Add("@Mail", createCompanyInformationDto.Mail);
            parameters.Add("@Description", createCompanyInformationDto.Description);
            parameters.Add("@PhoneNumber", createCompanyInformationDto.PhoneNumber);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
        public async Task RemoveCompanyInformationAsync(int id)
        {
            string query = "Delete from Company_Information where Id=@Id";
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task UpdateCompanyInformationAsync(UpdateCompanyInformationDto updateCompanyInformationDto)
        {
            string query = "Update Company_Information Set Name=@Name, Address=@Address, Mail=@Mail, Description=@Description, PhoneNumber=@PhoneNumber where Id=@Id";
            var parameters = new DynamicParameters();
            parameters.Add("@Id", updateCompanyInformationDto.Id);
            parameters.Add("@Name", updateCompanyInformationDto.Name);
            parameters.Add("@Address", updateCompanyInformationDto.Address);
            parameters.Add("@Mail", updateCompanyInformationDto.Mail);
            parameters.Add("@Description", updateCompanyInformationDto.Description);
            parameters.Add("@PhoneNumber", updateCompanyInformationDto.PhoneNumber);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
