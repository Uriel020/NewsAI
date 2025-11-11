
using NewsAI.Core.Models.Category;

namespace NewsAI.Infrastructure.Services
{
    public interface ICategoryService: ICommonService<CategoryDto, CreateCategoryDto, UpdateCategoryDto>
    {
        
    }
}