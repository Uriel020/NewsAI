using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using NewsAI.Core.Entities;
using NewsAI.Core.Models.News;
using NewsAI.Core.Models.News.Validators;
using NewsAI.Infrastructure.Services;

namespace NewsAI.Api.Controllers
{
    [Route("api/news")]
    [ApiController]

    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;
        private readonly IValidator<CreateNewsDTO> _createNewsValidator;
        private readonly IValidator<UpdateNewsDTO> _updateNewsValidator;

        public NewsController(
            INewsService newsService,  
            IValidator<CreateNewsDTO> createNewsValidator
            ,IValidator<UpdateNewsDTO> updateNewsValidator
            )
        {
            _newsService = newsService;
            _createNewsValidator = createNewsValidator;
            _updateNewsValidator = updateNewsValidator;
        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<NewsDto>>> GetNews()
        {
            try
            {
                var newsList = await _newsService.FindAll();

                var cleanNews = newsList.Select(n => new NewsDto
                {
                    Id = n.Id,
                    CategoryId = n.CategoryId,
                    Title = n.Title,
                    Description = n.Description,
                    Url = n.Url,
                    Views = n.Views,
                    HotNews = n.HotNews
                });
                return Ok(cleanNews);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<NewsDto>> GetNewById(Guid id)
        {
            try
            {
                var news = await _newsService.FindById(id);

                if (news == null) return NotFound();

                var mapNews = new NewsDto
                {
                    Id = news.Id,
                    CategoryId = news.CategoryId,
                    Title = news.Title,
                    Description = news.Description,
                    Url = news.Url,
                    Views = news.Views,
                    HotNews = news.HotNews
                };

                return Ok(mapNews);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]

        public async Task<ActionResult<Guid>> AddNew(CreateNewsDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var validator = await _createNewsValidator.ValidateAsync(dto);
            
            if (!validator.IsValid) return BadRequest(validator);
            
            try
            {
                var newNews = new News
                {
                    Title = dto.Title,
                    Description = dto.Description,
                    Url = dto.Url,
                    CategoryId = dto.CategoryId
                };

                await _newsService.Create(newNews);

                return Created();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut("{id}")]

        public async Task<ActionResult<Guid>> UpdateNews(Guid id, UpdateNewsDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var validator = await _updateNewsValidator.ValidateAsync(dto);
            
            if (!validator.IsValid) return BadRequest(validator);
            
            try
            {
                var news = new News
                {
                    Title = dto.Title,
                    Description = dto.Description,
                    CategoryId = dto.CategoryId
                };

                var updatedNew = await _newsService.Update(news);

                if (!updatedNew) return NotFound();

                return Ok(news.Id);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteNews(Guid id)
        {
            try
            {
                var deletedNews = await _newsService.Delete(id);

                if (!deletedNews) return NotFound();

                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}