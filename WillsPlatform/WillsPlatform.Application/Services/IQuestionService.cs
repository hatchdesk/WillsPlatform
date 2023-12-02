using WillsPlatform.Application.DTOs;

namespace WillsPlatform.Application.Services
{
    public interface IQuestionService
    {
        Task<IEnumerable<QuestionDTO>> GetQuestionsAsync();
        Task<bool> AddQuestionAsync(QuestionDTO questionPostDTO);
        Task<QuestionDTO> GetQuestionsByIdAsync(int id);
        Task<bool> UpdateQuestionAsync(QuestionDTO questionPostDTO);
        Task<bool> DeleteQuestionAsync(int id);
    }
}
