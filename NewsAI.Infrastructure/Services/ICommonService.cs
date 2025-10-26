namespace NewsAI.Infrastructure.Services;

public interface ICommonService<T>
{
    List<string> Errors { get; }
    Task<IEnumerable<T>> FindAll();
    
    Task<T?> FindById(Guid id);

    Task<bool> Create(T entity);
    
    Task<bool> Update(T entity);

    Task<bool> Delete(Guid id);

    Task<bool> ValidateExist(Guid id);
}