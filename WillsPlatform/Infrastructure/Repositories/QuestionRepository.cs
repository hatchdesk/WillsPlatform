using Domains.Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WillsPlatform.Application.DTOs;
using WillsPlatform.Application.Repositories;
using WillsPlatform.Domains.Entities;

namespace WillsPlatform.Infrastructure.Repositories
{
    public sealed class QuestionRepository : BaseRepository<Question>, IQuestionRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public QuestionRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<QuestionDTO>> GetAllQuestionsByFormAsync(int formId)
        {
            var questions = await (from Ques in _dbContext.Questions
                             join Tok in _dbContext.Tokens on Ques.Id equals Tok.QuestionId
                             where Ques.FormId == formId
                             select new QuestionDTO
                             {
                                 Id = Ques.Id,
                                 Name = Tok.Name,
                                 Text = Ques.Text
                             }).ToListAsync();

            return questions;
        }

        public async Task<List<Question>> GetAllQuestionsAsync()
        {
            var query = await _dbContext.Questions
                        .Include(x=>x.Form)
                        .Include(x=>x.Field).ToListAsync();
            return query;
        }
    }
}
