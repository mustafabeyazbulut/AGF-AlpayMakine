using AlpayMakina.Dtos.SocialMediaDtos;

namespace AlpayMakina.Repositories.SocialMediaRepositories
{
	public interface ISocialMediaRepository
	{
		Task<List<ResultSocialMediaDto>> GetAllSocialMediaAsync();
		Task AddSocialMediaAsync(CreateSocialMediaDto createSocialMediaDto);
		Task RemoveSocialMediaAsync(int id);
		Task<UpdateSocialMediaDto> GetSocialMediaAsync(int id);
		Task UpdateSocialMediaAsync(UpdateSocialMediaDto updateSocialMediaDto);
	}
}
