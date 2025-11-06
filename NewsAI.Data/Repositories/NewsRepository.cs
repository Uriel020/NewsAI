using Microsoft.EntityFrameworkCore;
using NewsAI.Core.Entities;
using NewsAI.Data.Context;
using NewsAI.Infrastructure.Repositories;

namespace NewsAI.Data.Repositories;

public class NewsRepository : INewsRepository
{
    private readonly NewsDbContext _context;

    public NewsRepository(NewsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<News>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.News.ToListAsync(cancellationToken);
    }

    public async Task<News?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
       return await _context.News.FindAsync(id, cancellationToken);
    }

    public async Task<News> AddAsync(News news, CancellationToken cancellationToken = default)
    {
        var newNews = await _context.News.AddAsync(news, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return newNews.Entity;
    }

    public async Task<bool> UpdateAsync(News news, CancellationToken cancellationToken = default)
    {
        _context.News.Update(news);
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var news = new News { Id = id };
        _context.News.Attach(news);
        _context.News.Remove(news);

        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> SearchByTitle(string title, CancellationToken cancellationToken = default)
    {
        return await _context.News.AnyAsync(n => n.Title == title, cancellationToken);
    }
}