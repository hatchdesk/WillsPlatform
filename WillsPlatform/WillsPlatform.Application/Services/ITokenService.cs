using WillsPlatform.Application.DTOs;

namespace WillsPlatform.Application.Services
{
    public interface ITokenService
    {
        Task<bool> AddTokenAsync(List<TemplateDTO> tokensPostDto);
    }
}
