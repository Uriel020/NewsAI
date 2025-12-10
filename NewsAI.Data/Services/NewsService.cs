using AutoMapper;
using FluentValidation;
using NewsAI.Core.Common;
using NewsAI.Core.Entities;
using NewsAI.Core.Models.News;
using NewsAI.Infrastructure.Repositories;
using NewsAI.Infrastructure.Services;

namespace NewsAI.Data.Services;

public class NewsService : INewsService
{
    private readonly INewsRepository _newsRepository;
    private readonly IValidator<CreateNewsDto> _createNewsValidator;
    private readonly IValidator<UpdateNewsDto> _updateNewsValidator;
    private readonly IMapper _newsMapper;

    public NewsService(
        INewsRepository newsRepository,
        IValidator<CreateNewsDto> createNewsValidator,
        IValidator<UpdateNewsDto> updateNewsValidator,
        IMapper newsMapper
        )
    {
        _newsRepository = newsRepository;
        _createNewsValidator = createNewsValidator;
        _updateNewsValidator = updateNewsValidator;
        _newsMapper = newsMapper;
    }


    public async Task<Result<IEnumerable<NewsDto>>> FindAll()
    {
        var news = await _newsRepository.GetAllAsync();

        var mapNews = _newsMapper.Map<IEnumerable<NewsDto>>(news);

        return Result<IEnumerable<NewsDto>>.Success(mapNews);
    }

    public async Task<Result<NewsDto?>> FindById(Guid id)
    {
        await ValidateExist(id);

        var news = await _newsRepository.GetByIdAsync(id);

        var mapNews = _newsMapper.Map<NewsDto>(news);

        return Result<NewsDto?>.Success(mapNews);
    }

    public async Task<Result<Guid>> Create(CreateNewsDto news)
    {
        var titleExist = await _newsRepository.SearchByTitle(news.Title);

        if (titleExist) return Result<Guid>.Conflict("Title already exist");

        var validateNews = _createNewsValidator.Validate(news);

        if (!validateNews.IsValid) return Result<Guid>.BadRequest(validateNews.Errors);

        var mapNews = _newsMapper.Map<News>(news);

        News newNews = await _newsRepository.AddAsync(mapNews);

        return Result<Guid>.Success(newNews.Id);
    }

    public async Task<Result<bool>> Update(Guid id, UpdateNewsDto news)
    {
        News? existingNews = await _newsRepository.GetByIdAsync(id);

        if (existingNews == null) return Result<bool>.NotFound("News not found");

        if (!string.IsNullOrWhiteSpace(news.Title) && news.Title != existingNews!.Title)
        {
            var titleExist = await _newsRepository.SearchByTitle(news.Title);
            if (titleExist) return Result<bool>.Conflict("Title already exist");
        }

        var validateNews = _updateNewsValidator.Validate(news);

        if (!validateNews.IsValid) return Result<bool>.BadRequest(validateNews.Errors);

        var mapNews = _newsMapper.Map<News>(news);

        await _newsRepository.UpdateAsync(mapNews);

        return Result<bool>.Success(true);

    }

    public async Task<Result<bool>> Delete(Guid id)
    {
        var exist = await ValidateExist(id);

        if (!exist.IsSuccess) return Result<bool>.NotFound(exist.Error!);

        await _newsRepository.DeleteAsync(exist.Value.Id);

        return Result<bool>.Success(true);
    }

    public async Task<Result<News>> ValidateExist(Guid id)
    {
        var exist = await _newsRepository.GetByIdAsync(id);

        if (exist == null) return Result<News>.NotFound("News not found");

        return Result<News>.Success(exist);
    }

    public Task<Result<Category>> SearchCategory(Guid id)
    {
        throw new NotImplementedException();
    }
}