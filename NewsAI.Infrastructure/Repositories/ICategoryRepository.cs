using NewsAI.Core.Entities;

namespace NewsAI.Infrastructure.Repositories
{
    public interface ICategoryRepository : ICommonRepository<Category>
    {
        public Task<bool> NameExist (string categoryName, CancellationToken cancellationToken = default);
    }
}