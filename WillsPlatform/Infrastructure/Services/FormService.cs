using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WillsPlatform.Application.DTOs;
using WillsPlatform.Application.Repositories;
using WillsPlatform.Application.Services;
using WillsPlatform.Domains.Entities;

namespace WillsPlatform.Infrastructure.Services
{
    public class FormService : IFormService
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly ITemplateRepository _templateRepository;
        private readonly IFormRepository _formRepository;
        private readonly IMapper _mapper;

        public FormService(IQuestionRepository questionRepository, ITemplateRepository templateRepository, IMapper mapper, IFormRepository formRepository)
        {
            _mapper = mapper;
            _questionRepository = questionRepository;
            _templateRepository = templateRepository;
            _formRepository = formRepository;
        }
    
        public async Task<IEnumerable<QuestionDTO>> GetFormQuestionsAsync(int formId)
        {
            var questions = await _questionRepository.GetAllQuestionsByFormAsync(formId);
            return questions;
        }

        public async Task<TemplateDTO> GetFormTemplateAsync(int formId)
        {
            var query = _templateRepository.GetAllQueryable(includes: x=>x.Tokens);
            var template = await query.Where(q => q.FormId == formId).FirstOrDefaultAsync();
            return _mapper.Map<Template,TemplateDTO>(template??new Template());
        }

        public async Task<IEnumerable<FormDTO>> GetAllFormAsync()
        {
            var forms = await _formRepository.GetAllAsync();
            return _mapper.Map<List<FormDTO>>(forms);
        }

    }
}
