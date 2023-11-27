using AutoMapper;
using WillsPlatform.Application.DTOs;
using WillsPlatform.Application.Repositories;
using WillsPlatform.Application.Services;

namespace WillsPlatform.Infrastructure.Services
{
    public class FieldService : IFieldService
    {
        private readonly IFieldRepository _fieldRepository;
        private readonly IMapper _mapper;
        public FieldService(IFieldRepository fieldRepository,IMapper mapper)
        {
            _fieldRepository = fieldRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<FieldDTO>> GetAllFieldAsync()
        {
            var fields = await _fieldRepository.GetAllAsync();
            return _mapper.Map<List<FieldDTO>>(fields);
        }
    }
}
