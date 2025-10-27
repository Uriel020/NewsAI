using NewsAI.Core.Entities;
using NewsAI.Core.Models.News;

namespace NewsAI.Infrastructure.Services;

public interface INewsService: ICommonService<NewsDto, CreateNewsDTO, UpdateNewsDTO>
{
     
}