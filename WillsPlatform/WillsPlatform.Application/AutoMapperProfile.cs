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
            CreateMap<Question, QuestionDTO>()
            .ForMember(dest => dest.Form, opt => opt.MapFrom(src => src.Form))
            .ForMember(dest => dest.Field, opt => opt.MapFrom(src => src.Field));
            CreateMap<Token, TokenDTO>();
            CreateMap<Form, FormDTO>();
            CreateMap<Field, FieldDTO>();
            CreateMap<Template, TemplateDTO>().ForMember(d=>d.Tokens, s => s.MapFrom(x => x.Tokens));
            CreateMap<QuestionDTO, Question>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<FormDTO, Form>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
