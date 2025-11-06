using NewsAI.Core.Common;
using NewsAI.Core.Models.News;

namespace NewsAI.Infrastructure.Services;

public interface INewsService : ICommonService<NewsDto, CreateNewsDto, UpdateNewsDto>
{
    Task<Result<bool>> SearchCategory(Guid id);
}