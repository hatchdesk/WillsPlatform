using AutoMapper;
using WillsPlatform.Application;
using WillsPlatform.Application.DTOs;
using WillsPlatform.Application.Repositories;
using WillsPlatform.Application.Services;
using WillsPlatform.Domains.Entities;

namespace WillsPlatform.Infrastructure.Services
{
    public class TemplateService : ITemplateService
    {
        private readonly ITemplateRepository _templateRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public TemplateService(ITemplateRepository templateRepository,IMapper mapper, IUnitOfWork unitOfWork)
        {
            _templateRepository = templateRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<TemplateDTO>> GetAllTEmplateAsync()
        {
            var templates = await _templateRepository.GetAllAsync();
            return _mapper.Map<List<TemplateDTO>>(templates);
        }

        public async Task<bool> AddTemplateAsync(TemplateDTO questionPostDTO)
        {
            try
            {
                var template = _mapper.Map<Template>(questionPostDTO);
                await _templateRepository.AddAsync(template);
                var addedRecord = await _unitOfWork.SaveChangesAsync();
                return (addedRecord > 0);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<TemplateDTO> GetTemplateByIdAsync(int id)
        {
            var template = await _templateRepository.GetAsync(id);
            return _mapper.Map<TemplateDTO>(template);
        }

        public async Task<bool> UpdateTemplateAsync(TemplateDTO templatePostDTO)
        {
            try
            {
                var template = await _templateRepository.GetAsync(templatePostDTO.Id);
                template.Text = templatePostDTO.Text;
                template.FormId = templatePostDTO.FormId;
                await _templateRepository.UpdateAsync(template);
                var updatedRecord = await _unitOfWork.SaveChangesAsync();
                return (updatedRecord > 0);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeleteTemplateAsync(int id)
        {
            try
            {
                var templateDTO = await _templateRepository.GetAsync(id);
                await _templateRepository.DeleteAsync(templateDTO);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
