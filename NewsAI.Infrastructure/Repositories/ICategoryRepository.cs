using NewsAI.Core.Entities;

namespace NewsAI.Infrastructure.Repositories
{
    public interface ICategoryRepository : ICommonRepository<Category>
    {
        public Task<bool> FindName (string categoryName, CancellationToken cancellationToken = default);
    }
}