using System;
using WillsPlatform.Application.DTOs;


namespace WillsPlatform.Application.Services
{
    public interface IFieldService
    {
        Task<IEnumerable<FieldDTO>> GetAllFieldAsync();
    }
}
