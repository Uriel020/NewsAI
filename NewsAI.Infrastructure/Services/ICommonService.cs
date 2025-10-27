using NewsAI.Core.Common;

namespace NewsAI.Infrastructure.Services;

public interface ICommonService<TDto, TCreateDto, TUpdateDto>
{
    Task<Result<IEnumerable<TDto>>> FindAll();

    Task<Result<TDto?>> FindById(Guid id);

    Task<Result<TDto>> Create(TCreateDto entity);

    Task<Result<TDto>> Update(Guid id, TUpdateDto entity);

    Task<Result<TDto>> Delete(Guid id);

    Task<bool> ValidateExist(Guid id);
}