using FluentValidation;
using NewsAI.Core.Common;
using NewsAI.Core.Entities;
using NewsAI.Core.Models.News;
using NewsAI.Core.Models.News.Validators;
using NewsAI.Infrastructure.Repositories;
using NewsAI.Infrastructure.Services;

namespace NewsAI.Data.Service;

public class NewsService : INewsService
{
    private readonly INewsRepository _newsRepository;
    private readonly IValidator<CreateNewsDTO> _createNewsValidator;
    private readonly IValidator<UpdateNewsDTO> _updateNewsValidator;

    public NewsService(
        INewsRepository newsRepository, 
        IValidator<CreateNewsDTO> createNewsValidator,
        IValidator<UpdateNewsDTO> updateNewsValidator
        )
    {
        _newsRepository = newsRepository;
        _createNewsValidator = createNewsValidator;
        _updateNewsValidator = updateNewsValidator;
    }


    public async Task<Result<IEnumerable<NewsDto>>> FindAll()
    {
        var news = await _newsRepository.GetNewsAsync();

        var mapNews = news.Select(n => new NewsDto
        {
            Id = n.Id,
            Title = n.Title,
            Description = n.Description,
            CategoryId = n.CategoryId,
            Url = n.Url,
            HotNews = n.HotNews,
            Views = n.Views,
        });

        return Result<IEnumerable<NewsDto>>.Success(mapNews);
    }

    public async Task<Result<NewsDto?>> FindById(Guid id)
    {
        var newExist = await ValidateExist(id);

        if (!newExist) return Result<NewsDto?>.Failure("Not found");

        var news = await _newsRepository.GetNewsByIdAsync(id);

        var mapNews = new NewsDto
        {
            Id = news!.Id,
            Title = news.Title,
            Description = news.Description,
            CategoryId = news.CategoryId,
            Url = news.Url,
            HotNews = news.HotNews,
            Views = news.Views,
        };

        return Result<NewsDto?>.Success(mapNews);
    }

    public async Task<Result<NewsDto>> Create(CreateNewsDTO news)
    {
        var titleExist = await _newsRepository.SearchByTitle(news.Title);

        if (titleExist) return Result<NewsDto>.Failure("Title already exist");

        var validateNews = _createNewsValidator.Validate(news);
        
        if (!validateNews.IsValid) return Result<NewsDto>.Failure("Invalid data");

        var mapNew = new News
        {
            Title = news.Title,
            Description = news.Description,
            CategoryId = news.CategoryId,
            Url = news.Url,
        };
        
        var newNews = await _newsRepository.AddNewsAsync(mapNew);
        
        return Result<NewsDto>.Success();
    }

    public async Task<Result<NewsDto>> Update(Guid id, UpdateNewsDTO news)
    {
        var existNew = await ValidateExist(id);

        if (!existNew) return false;

        var titleExist = await _newsRepository.SearchByTitle(news.Title);

        if (!titleExist) return await _newsRepository.UpdateNewsAsync(news);

        return false;
    }

    public async Task<Result<NewsDto>> Delete(Guid id)
    {
        var existNew = await ValidateExist(id);

        if (!existNew) return false;

        return await _newsRepository.DeleteNewsAsync(id);
    }

    public async Task<bool> ValidateExist(Guid id)
    {
        var exist = await _newsRepository.GetNewsByIdAsync(id);

        return exist != null;
    }
}