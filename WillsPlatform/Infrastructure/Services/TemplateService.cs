using AutoMapper;
using WillsPlatform.Application.DTOs;
using WillsPlatform.Application.Repositories;
using WillsPlatform.Application.Services;
using WillsPlatform.Infrastructure.Repositories;

namespace WillsPlatform.Infrastructure.Services
{
    public class TemplateService : ITemplateService
    {
        private readonly ITemplateRepository _templateRepository;
        private readonly IMapper _mapper;
        public TemplateService(ITemplateRepository templateRepository,IMapper mapper)
        {
            _templateRepository = templateRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<TemplateDTO>> GetAllTEmplateAsync()
        {
            var templates = await _templateRepository.GetAllAsync();
            return _mapper.Map<List<TemplateDTO>>(templates);
        }
    }
}
