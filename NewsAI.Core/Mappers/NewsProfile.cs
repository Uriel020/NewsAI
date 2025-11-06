using AutoMapper;
using NewsAI.Core.Entities;
using NewsAI.Core.Models.News;

namespace NewsAI.Core.Mappers;

public class NewsProfile : Profile
{
    public NewsProfile()
    {
        CreateMap<News, NewsDto>();
        CreateMap<CreateNewsDto, News>();
        CreateMap<UpdateNewsDto, News>()
        .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
    }
}