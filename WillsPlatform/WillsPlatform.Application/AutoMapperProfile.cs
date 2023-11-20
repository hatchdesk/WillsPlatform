using AutoMapper;
using Domains.Entities;
using WillsPlatform.Application.DTOs;
using WillsPlatform.Domains.Entities;

namespace WillsPlatform.Application
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Question, QuestionDTO>();
            CreateMap<Token, TokenDTO>();
            CreateMap<Template, TemplateDTO>().ForMember(d=>d.Tokens, s => s.MapFrom(x => x.Tokens));
        }
    }
}
