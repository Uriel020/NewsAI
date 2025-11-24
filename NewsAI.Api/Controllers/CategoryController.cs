using Microsoft.AspNetCore.Mvc;
using NewsAI.Core.Common;
using NewsAI.Core.Models.Category;
using NewsAI.Infrastructure.Services;

namespace NewsAI.Api.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
        {
            var categories = await _categoryService.FindAll();

            return Ok(categories.Value);
        }

        [HttpGet("{id:guid}")]

        public async Task<ActionResult<CategoryDto>> GetCategoryById (Guid id)
        {
            var category = await _categoryService.FindById(id);

            if(category.HttpErrorType == HttpErrorType.NotFound ) return NotFound(category.Error);

            return Ok(category.Value);
        }
    }
}