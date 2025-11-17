using NewsAI.Core.Common;

namespace NewsAI.Infrastructure.Services;

public interface ICommonService<TDto, TCreateDto, TUpdateDto>
{
    Task<Result<IEnumerable<TDto>>> FindAll();

    Task<Result<TDto?>> FindById(Guid id);

    Task<Result<Guid>> Create(TCreateDto entity);

    Task<Result<bool>> Update(Guid id, TUpdateDto entity);

    Task <Result<bool>> Delete(Guid id);

    Task<Result<bool>> ValidateExist(Guid id);
}