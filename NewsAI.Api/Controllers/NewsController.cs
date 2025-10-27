using Microsoft.AspNetCore.Mvc;
using NewsAI.Core.Entities;
using NewsAI.Core.Models.News;
using NewsAI.Infrastructure.Services;

namespace NewsAI.Api.Controllers
{
    [Route("api/news")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;


        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NewsDto>>> GetNews()
        {
            var newsList = await _newsService.FindAll();

            return Ok(newsList);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<NewsDto>> GetNewById(Guid id)
        {
            var news = await _newsService.FindById(id);

            return Ok(news);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> AddNew(CreateNewsDTO newNews)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _newsService.Create(newNews);

            return Created();
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Guid>> UpdateNews(Guid id, UpdateNewsDTO news)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            news = await _newsService.Update(id, news);

            return Ok(news);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteNews(Guid id)
        {
            await _newsService.Delete(id);

            return NoContent();
        }
    }
}