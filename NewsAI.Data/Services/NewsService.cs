using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
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
    private readonly IMapper _mapper;

    public NewsService(
        INewsRepository newsRepository,
        IValidator<CreateNewsDTO> createNewsValidator,
        IValidator<UpdateNewsDTO> updateNewsValidator,
        IMapper mapper
        )
    {
        _newsRepository = newsRepository;
        _createNewsValidator = createNewsValidator;
        _updateNewsValidator = updateNewsValidator;
        _mapper = mapper;
    }


    public async Task<Result<IEnumerable<NewsDto>>> FindAll()
    {
        var news = await _newsRepository.GetNewsAsync();

        var mapNews = _mapper.Map<IEnumerable<NewsDto>>(news);

        return Result<IEnumerable<NewsDto>>.Success(mapNews);
    }

    public async Task<Result<NewsDto?>> FindById(Guid id)
    {
        var newExist = await ValidateExist(id);

        if (!newExist) return Result<NewsDto?>.Failure("News not found");

        var news = await _newsRepository.GetNewsByIdAsync(id);

        var mapNews = _mapper.Map<NewsDto>(news);

        return Result<NewsDto?>.Success(mapNews);
    }

    public async Task<Result<Guid>> Create(CreateNewsDTO news)
    {
        var titleExist = await _newsRepository.SearchByTitle(news.Title);

        if (titleExist) return Result<Guid>.Failure("Title already exist");

        var validateNews = _createNewsValidator.Validate(news);

        if (!validateNews.IsValid) return Result<Guid>.Failure(validateNews.Errors);

        var mapNews = _mapper.Map<News>(news);

        News newNews = await _newsRepository.AddNewsAsync(mapNews);

        return Result<Guid>.Success(newNews.Id);
    }

    public async Task<Result<bool>> Update(Guid id, UpdateNewsDTO news)
    {
        News? existingNews = await _newsRepository.GetNewsByIdAsync(id);

        if (existingNews == null) return Result<bool>.Failure("News not found");

        if (!string.IsNullOrWhiteSpace(news.Title) && news.Title != existingNews!.Title)
        {
            var titleExist = await _newsRepository.SearchByTitle(news.Title);
            if (titleExist) return Result<bool>.Failure("Title already exist");
        }

        var validateNews = _updateNewsValidator.Validate(news);

        if (!validateNews.IsValid) return Result<bool>.Failure(validateNews.Errors);

        var mapNews = _mapper.Map<News>(news);

        await _newsRepository.UpdateNewsAsync(mapNews);

        return Result<bool>.Success(true);

    }

    public async Task<Result<bool>> Delete(Guid id)
    {
        var existNew = await ValidateExist(id);

        if (!existNew) return Result<bool>.Failure("News not found");

        await _newsRepository.DeleteNewsAsync(id);

        return Result<bool>.Success(true);
    }

    public async Task<bool> ValidateExist(Guid id)
    {
        var exist = await _newsRepository.GetNewsByIdAsync(id);

        return exist != null;
    }
}