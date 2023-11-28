using WillsPlatform.Application.DTOs;

namespace WillsPlatform.Application.Services
{
    public interface IQuestionService
    {
        Task<IEnumerable<QuestionDTO>> GetQuestionsAsync();
        Task<bool> CreateQuestionAsync(QuestionDTO questionPostDTO);
        Task<QuestionDTO> GetQuestionsByIdAsync(int id);
        Task<bool> UpdateQuestionAsync(QuestionDTO questionPostDTO);
    }
}
