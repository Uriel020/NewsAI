

using NewsAI.Core.Entities;

namespace NewsAI.Infrastructure.Repositories;

public interface INewsRepository : ICommonRepository<News>
{
    public Task<bool> SearchByTitle(string title, CancellationToken cancellationToken = default);

}