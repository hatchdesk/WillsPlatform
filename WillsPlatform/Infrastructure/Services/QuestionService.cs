using AutoMapper;
using Domains.Entities;
using WillsPlatform.Application.DTOs;
using WillsPlatform.Application.Repositories;
using WillsPlatform.Application.Services;

namespace WillsPlatform.Infrastructure.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IMapper _mapper;

        public QuestionService(IQuestionRepository questionRepository, IMapper mapper)
        {
            _questionRepository = questionRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<QuestionDTO>> GetQuestionsAsync()
        {
            var questions = await _questionRepository.GetAllQuestionsAsync();
            return _mapper.Map<List<QuestionDTO>>(questions);
        }

        public async Task<bool> CreateQuestionAsync(QuestionDTO questionPostDTO)
        {
            try
            {
                var question = _mapper.Map<Question>(questionPostDTO);
                await _questionRepository.AddAsync(question);
                return true;
            }
            catch (Exception ex) 
            { 
                return false;
            }
        }

        public async Task<QuestionDTO> GetQuestionsByIdAsync(int id)
        {
            var questions = await _questionRepository.GetAsync(id);
            return _mapper.Map<QuestionDTO>(questions);
        }

        public async Task<bool> UpdateQuestionAsync(QuestionDTO questionPostDTO)
        {
            try
            {
                var question = _mapper.Map<Question>(questionPostDTO);
                await _questionRepository.UpdateAsync(question);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
