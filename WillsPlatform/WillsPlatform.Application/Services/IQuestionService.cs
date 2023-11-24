using WillsPlatform.Application.DTOs;

namespace WillsPlatform.Application.Services
{
    public interface IQuestionService
    {
        Task<IEnumerable<QuestionDTO>> GetQuestionsAsync();
    }
}
