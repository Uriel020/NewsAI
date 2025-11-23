using Microsoft.AspNetCore.Mvc;
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
    }
}