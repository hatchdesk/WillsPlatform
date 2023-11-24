using Domains.Entities;
using WillsPlatform.Application.DTOs;

namespace WillsPlatform.Application.Repositories
{
    public interface IQuestionRepository : IRepository<Question>
    {
        Task<IEnumerable<QuestionDTO>> GetAllQuestionsByFormAsync(int formId);
        Task<IEnumerable<Question>> GetAllQuestionsAsync();
    }
}
