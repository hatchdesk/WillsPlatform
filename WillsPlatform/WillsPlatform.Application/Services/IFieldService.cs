using System;
using WillsPlatform.Application.DTOs;


namespace WillsPlatform.Application.Services
{
    public interface IFieldService
    {
        Task<IEnumerable<FieldDTO>> GetAllFieldAsync();
        Task<bool> AddFieldAsync(FieldDTO fieldDTO);
        Task<FieldDTO> GetFieldByIdAsync(int id);
        Task<bool> UpdateFieldAsync(FieldDTO fieldDTO);
    }
}
