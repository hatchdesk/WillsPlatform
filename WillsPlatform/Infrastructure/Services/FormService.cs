using AutoMapper;
using Domains.Entities;
using Microsoft.EntityFrameworkCore;
using WillsPlatform.Application;
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
        private readonly IUnitOfWork _unitOfWork;

        public FormService(IQuestionRepository questionRepository, ITemplateRepository templateRepository, IMapper mapper, IFormRepository formRepository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _questionRepository = questionRepository;
            _templateRepository = templateRepository;
            _formRepository = formRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<QuestionDTO>> GetFormQuestionsAsync(int formId)
        {
            var questions = await _questionRepository.GetAllQuestionsByFormAsync(formId);
            return questions;
        }

        public async Task<TemplateDTO> GetFormTemplateAsync(int formId)
        {
            var query = _templateRepository.GetAllQueryable(includes: x => x.Tokens);
            var template = await query.Where(q => q.FormId == formId).FirstOrDefaultAsync();
            return _mapper.Map<Template, TemplateDTO>(template ?? new Template());
        }

        public async Task<IEnumerable<FormDTO>> GetAllFormAsync()
        {
            var forms = await _formRepository.GetAllAsync();
            return _mapper.Map<List<FormDTO>>(forms);
        }

        public async Task<bool> AddFormAsync(FormDTO questionPostDTO)
        {
            try
            {
                var form = _mapper.Map<Form>(questionPostDTO);
                await _formRepository.AddAsync(form);
                var addedRecord = await _unitOfWork.SaveChangesAsync();
                return (addedRecord > 0);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<FormDTO> GetFormByIdAsync(int id)
        {
            var form = await _formRepository.GetAsync(id);
            return _mapper.Map<FormDTO>(form);
        }

        public async Task<bool> UpdateFormAsync(FormDTO formPostDTO)
        {
            try
            {
                var form = await _formRepository.GetAsync(formPostDTO.Id);
                form.Name = formPostDTO.Name;
                await _formRepository.UpdateAsync(form);
                var updatedRecord = await _unitOfWork.SaveChangesAsync();
                return (updatedRecord > 0);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}