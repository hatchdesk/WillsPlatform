using WillsPlatform.Application.DTOs;

namespace WillsPlatform.Application.Services
{
    public interface IQuestionService
    {
        Task<List<QuestionDTO>> GetQuestionsAsync();
    }
}
