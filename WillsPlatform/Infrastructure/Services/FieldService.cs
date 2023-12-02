using AutoMapper;
using Domains.Entities;
using WillsPlatform.Application;
using WillsPlatform.Application.DTOs;
using WillsPlatform.Application.Repositories;
using WillsPlatform.Application.Services;

namespace WillsPlatform.Infrastructure.Services
{
    public class FieldService : IFieldService
    {
        private readonly IFieldRepository _fieldRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public FieldService(IFieldRepository fieldRepository,IMapper mapper, IUnitOfWork unitOfWork)
        {
            _fieldRepository = fieldRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AddFieldAsync(FieldDTO fieldDTO)
        {
            try
            {
                var field = _mapper.Map<Field>(fieldDTO);
                await _fieldRepository.AddAsync(field);
                var addedRecord = await _unitOfWork.SaveChangesAsync();
                return (addedRecord > 0);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<IEnumerable<FieldDTO>> GetAllFieldAsync()
        {
            var fields = await _fieldRepository.GetAllAsync();
            return _mapper.Map<List<FieldDTO>>(fields);
        }

        public async Task<FieldDTO> GetFieldByIdAsync(int id)
        {
            var field = await _fieldRepository.GetAsync(id);
            return _mapper.Map<FieldDTO>(field);
        }

        public async Task<bool> UpdateFieldAsync(FieldDTO fieldDTO)
        {
            try
            {
                var field = await _fieldRepository.GetAsync(fieldDTO.Id);
                field.Name = fieldDTO.Name;
                await _fieldRepository.UpdateAsync(field);
                var updatedRecord = await _unitOfWork.SaveChangesAsync();
                return (updatedRecord > 0);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> DeleteFieldAsync(int id)
        {
            try
            {
                _fieldRepository.Delete(id);
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
