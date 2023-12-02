using System.Threading.Tasks;
using WillsPlatform.Application.DTOs;

namespace WillsPlatform.Application.Services
{
    public interface IFormService
    {
        Task<IEnumerable<QuestionDTO>> GetFormQuestionsAsync(int formId);
        Task<TemplateDTO> GetFormTemplateAsync(int formId);
        Task<IEnumerable<FormDTO>> GetAllFormAsync();
        Task<bool> AddFormAsync(FormDTO questionPostDTO);
        Task<FormDTO> GetFormByIdAsync(int id);
        Task<bool> UpdateFormAsync(FormDTO questionPostDTO);
        Task<bool> DeleteFormAsync(int id);
    }
}
