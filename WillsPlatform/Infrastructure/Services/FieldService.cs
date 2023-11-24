using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WillsPlatform.Application.DTOs;
using WillsPlatform.Application.Repositories;
using WillsPlatform.Application.Services;
using WillsPlatform.Infrastructure.Repositories;

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
