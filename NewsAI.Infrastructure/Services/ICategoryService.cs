
using NewsAI.Core.Entities;
using NewsAI.Core.Models.Category;

namespace NewsAI.Infrastructure.Services
{
    public interface ICategoryService: ICommonService<Category,CategoryDto, CreateCategoryDto, UpdateCategoryDto>
    {
        
    }
}