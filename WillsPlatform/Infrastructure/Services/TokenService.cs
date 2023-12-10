using AutoMapper;
using WillsPlatform.Application;
using WillsPlatform.Application.DTOs;
using WillsPlatform.Application.Repositories;
using WillsPlatform.Application.Services;
using WillsPlatform.Domains.Entities;
using WillsPlatform.Infrastructure.Repositories;

namespace WillsPlatform.Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly ITokenRepository _tokenRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public TokenService(ITokenRepository tokenRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _tokenRepository = tokenRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AddTokenAsync(List<TemplateDTO> tokensPostDto)
        {
            try
            {
                var tokens = _mapper.Map<List<Token>>(tokensPostDto);
                await _tokenRepository.AddRangeAsync(tokens);
                var addedRecord = await _unitOfWork.SaveChangesAsync();
                return (addedRecord > 0);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
