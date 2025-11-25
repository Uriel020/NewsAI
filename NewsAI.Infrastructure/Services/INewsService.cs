using NewsAI.Core.Common;
using NewsAI.Core.Entities;
using NewsAI.Core.Models.News;

namespace NewsAI.Infrastructure.Services;

public interface INewsService : ICommonService<News,NewsDto, CreateNewsDto, UpdateNewsDto>
{
    Task<Result<Category>> SearchCategory(Guid id);
}