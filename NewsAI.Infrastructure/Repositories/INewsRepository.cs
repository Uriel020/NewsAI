
using NewsAI.Core.Entities;
using NewsAI.Core.Models;
using NewsAI.Core.Models.News;

namespace NewsAI.Infrastructure.Repositories;

public interface INewsRepository
{
    public Task<IEnumerable<News>> GetNewsAsync (CancellationToken cancellationToken = default);

    public Task<News?> GetNewsByIdAsync(Guid id, CancellationToken cancellationToken = default);
    
    public Task<bool> AddNewsAsync(News news ,CancellationToken cancellationToken = default);

    public Task<bool> UpdateNewsAsync(News news ,CancellationToken cancellationToken = default);

    public Task<bool> DeleteNewsAsync(Guid id, CancellationToken cancellationToken = default);
    
    public Task<bool> SearchByTitle(string title, CancellationToken cancellationToken = default);
}