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
    private readonly IMapper _mapper;

    public NewsService(
        INewsRepository newsRepository,
        IValidator<CreateNewsDto> createNewsValidator,
        IValidator<UpdateNewsDto> updateNewsValidator,
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
        var news = await _newsRepository.GetAllAsync();

        var mapNews = _mapper.Map<IEnumerable<NewsDto>>(news);

        return Result<IEnumerable<NewsDto>>.Success(mapNews);
    }

    public async Task<Result<NewsDto?>> FindById(Guid id)
    {
        var newExist = await ValidateExist(id);

        if (!newExist) return Result<NewsDto?>.NotFound("News not found with id: " + id);

        var news = await _newsRepository.GetByIdAsync(id);

        var mapNews = _mapper.Map<NewsDto>(news);

        return Result<NewsDto?>.Success(mapNews);
    }

    public async Task<Result<Guid>> Create(CreateNewsDto news)
    {
        var titleExist = await _newsRepository.SearchByTitle(news.Title);

        if (titleExist) return Result<Guid>.Conflict("Title already exist");

        var validateNews = _createNewsValidator.Validate(news);

        if (!validateNews.IsValid) return Result<Guid>.Failure(validateNews.Errors);

        var mapNews = _mapper.Map<News>(news);

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

        if (!validateNews.IsValid) return Result<bool>.Failure(validateNews.Errors);

        var mapNews = _mapper.Map<News>(news);

        await _newsRepository.UpdateAsync(mapNews);

        return Result<bool>.Success(true);

    }

    public async Task<Result<bool>> Delete(Guid id)
    {
        var existNew = await ValidateExist(id);

        if (!existNew) return Result<bool>.NotFound("News not found");

        await _newsRepository.DeleteAsync(id);

        return Result<bool>.Success(true);
    }

    public async Task<bool> ValidateExist(Guid id)
    {
        var exist = await _newsRepository.GetByIdAsync(id);

        return exist != null;
    }

    public Task<Result<bool>> SearchCategory(Guid id)
    {
        throw new NotImplementedException();
    }
}