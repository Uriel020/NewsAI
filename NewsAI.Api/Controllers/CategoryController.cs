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

        public async Task<ActionResult<CategoryDto>> GetCategoryById(Guid id)
        {
            var category = await _categoryService.FindById(id);
            return category.HttpErrorType == HttpErrorType.NotFound ? NotFound(category.Error) : Ok(category.Value);
        }

        [HttpPost]

        public async Task<ActionResult<Guid>> CreateCategory (CreateCategoryDto category)
        {
            var newCategory = await _categoryService.Create(category);

            if(newCategory.HttpErrorType != HttpErrorType.None)
            {
                return newCategory.HttpErrorType switch
                {
                    HttpErrorType.BadRequest => BadRequest(newCategory.Errors),
                    HttpErrorType.Conflict => Conflict(newCategory.Error),
                    _ => StatusCode(500, "An unexpected error occurred")
                };
            }

            return Created(newCategory.Value);
        }

        [HttpPut("{id:guid}")]

        public async Task<IActionResult> UpdateCategory(Guid id, UpdateCategoryDto category)
        {
            var updateCategory = await _categoryService.Update(id, category);

            if (updateCategory.HttpErrorType != HttpErrorType.None)
            {
                return updateCategory.HttpErrorType switch
                {
                    HttpErrorType.NotFound => NotFound(updateCategory.Error),
                    HttpErrorType.BadRequest => BadRequest(updateCategory.Errors),
                    HttpErrorType.Conflict => Conflict(updateCategory.Error),

                    _ => StatusCode(500, "An unexpected error occurred")
                };
            }
            return Accepted();
        }

        [HttpDelete("{id:guid}")]

        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            var deletedCategory = await _categoryService.Delete(id);

            return deletedCategory.HttpErrorType == HttpErrorType.NotFound ? NotFound(deletedCategory.Error) : Accepted();
        }
    }
}
