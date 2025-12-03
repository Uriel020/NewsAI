using Microsoft.EntityFrameworkCore;
using NewsAI.Core.Entities;
using NewsAI.Data.Context;
using NewsAI.Infrastructure.Repositories;

namespace NewsAI.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly NewsDbContext _context;
        public CategoryRepository(NewsDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Categories.ToListAsync(cancellationToken);
        }

        public async Task<Category?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Categories.FindAsync(id, cancellationToken);
        }

        public async Task<Category> AddAsync(Category entity, CancellationToken cancellationToken = default)
        {
            var newCategory = await _context.Categories.AddAsync(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return newCategory.Entity;
        }

        public async Task<bool> UpdateAsync(Category entity, CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var category = await _context.Categories.FindAsync(id);
            _context.Categories.Remove(category!);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> FindName (string categoryName, CancellationToken cancellationToken = default)
        {
            return await _context.Categories.AnyAsync(c => c.Name == categoryName, cancellationToken);

        }
    }
}