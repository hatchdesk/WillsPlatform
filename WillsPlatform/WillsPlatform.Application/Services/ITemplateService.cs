using WillsPlatform.Application.DTOs;

namespace WillsPlatform.Application.Services
{
    public interface ITemplateService
    {
        Task<IEnumerable<TemplateDTO>> GetAllTEmplateAsync();
    }
}
