using NewsAI.Core.Entities;
using NewsAI.Infrastructure.Repositories;
using NewsAI.Infrastructure.Services;

namespace NewsAI.Data.Service;

public class NewsService : INewsService
{
    private readonly INewsRepository _newsRepository;

    public NewsService(INewsRepository newsRepository)
    {
        _newsRepository = newsRepository;
    }


    public async Task<IEnumerable<News>> FindAll()
    {
        var news = await _newsRepository.GetNewsAsync();

        return news;
    }

    public async Task<News?> FindById(Guid id)
    {
        var newExist = await ValidateExist(id);
        
        if(!newExist) return null;

        var news = await _newsRepository.GetNewsByIdAsync(id);

        return news;
    }

    public async Task<bool> Create(News news)
    {
        var titleExist = await _newsRepository.SearchByTitle(news.Title);

        if (!titleExist) return await _newsRepository.AddNewsAsync(news);

        // Errors.Add($"Title already exist");
        return false;
    }

    public async Task<bool> Update(News news)
    {

        var existNew = await ValidateExist(news.Id);
        
        if (!existNew) return false;
        
        var titleExist = await _newsRepository.SearchByTitle(news.Title);

        if (!titleExist) return await _newsRepository.UpdateNewsAsync(news);

        // Errors.Add($"Title already exist");
        return false;
    }

    public async Task<bool> Delete(Guid id)
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