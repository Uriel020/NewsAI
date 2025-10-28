using Microsoft.AspNetCore.Mvc;
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

            if (news.Error != null)
            {
                return NotFound(news.Error);
            }

            return Ok(news);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> AddNew(CreateNewsDTO news)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var newNews = await _newsService.Create(news);

            if (newNews.Error != null) return BadRequest(newNews.Error);

            if (newNews.Errors != null) return BadRequest(newNews.Errors);

            return Created();
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Guid>> UpdateNews(Guid id, UpdateNewsDTO news)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updatedNews = await _newsService.Update(id, news);

            if (updatedNews.Errors != null) return BadRequest(updatedNews.Errors);

            return Ok(news);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteNews(Guid id)
        {
            var deletedNews = await _newsService.Delete(id);

            if (deletedNews.Error != null)
            {
                return NotFound(deletedNews.Error);
            }
            return NoContent();
        }
    }
}