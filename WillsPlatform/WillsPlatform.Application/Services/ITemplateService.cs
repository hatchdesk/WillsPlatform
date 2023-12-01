using WillsPlatform.Application.DTOs;

namespace WillsPlatform.Application.Services
{
    public interface ITemplateService
    {
        Task<IEnumerable<TemplateDTO>> GetAllTEmplateAsync();
        Task<bool> AddTemplateAsync(TemplateDTO questionPostDTO);
        Task<TemplateDTO> GetTemplateByIdAsync(int id);
        Task<bool> UpdateTemplateAsync(TemplateDTO templatePostDTO);
        Task<bool> DeleteTemplateAsync(int id);
    }
}
