using AutoMapper;
using NewsAI.Core.Entities;
using NewsAI.Core.Models.News;

namespace NewsAI.Core.Mappers;

public class NewsProfile : Profile
{
    public NewsProfile()
    {
        CreateMap<News, NewsDto>();
        CreateMap<CreateNewsDTO, News>();
        CreateMap<UpdateNewsDTO, News>();
    }
}