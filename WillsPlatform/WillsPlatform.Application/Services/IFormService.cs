using WillsPlatform.Application.DTOs;

namespace WillsPlatform.Application.Services
{
    public interface IFormService
    {
        Task<IEnumerable<QuestionDTO>> GetFormQuestionsAsync(int formId);
        Task<TemplateDTO> GetFormTemplateAsync(int formId);
    }
}
