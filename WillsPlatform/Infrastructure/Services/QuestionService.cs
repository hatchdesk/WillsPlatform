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
            _questionRepository = questionRepository ?? throw new ArgumentNullException(nameof(questionRepository)); 
            _mapper = mapper ?? throw new ArgumentNullException(nameof(_mapper));
        }

        public async Task<List<QuestionDTO>> GetQuestionsAsync()
        {
            var questionDto = new List<QuestionDTO>();
            var questions = await _questionRepository.GetAllQuestionsAsync();
            questionDto = _mapper.Map<List<QuestionDTO>>(questions);
            return questionDto;
        }
    }
}
