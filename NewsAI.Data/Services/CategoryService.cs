using NewsAI.Core.Common;
using NewsAI.Core.Models.Category;
using NewsAI.Infrastructure.Services;

namespace NewsAI.Data.Services
{
    public class CategoryService: ICategoryService
    {
        public Task<Result<IEnumerable<CategoryDto>>> FindAll()
        {
            throw new NotImplementedException();
        }

        public Task<Result<CategoryDto?>> FindById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<Guid>> Create(CreateCategoryDto entity)
        {
            throw new NotImplementedException();
        }

        public Task<Result<bool>> Update(Guid id, UpdateCategoryDto entity)
        {
            throw new NotImplementedException();
        }

        public Task<Result<bool>> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ValidateExist(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}