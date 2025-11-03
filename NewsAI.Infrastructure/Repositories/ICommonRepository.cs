namespace NewsAI.Infrastructure.Repositories
{
    public interface ICommonRepository<T>
    {
        public Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);

        public Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        public Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);

        public Task<bool> UpdateAsync(T entity, CancellationToken cancellationToken = default);

        public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    }
}