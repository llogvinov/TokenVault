using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TokenVault.Application.Common.Interfaces.Persistence;

namespace TokenVault.Infrastructure.Persistence.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly TokenVaultDbContext _dbContext;
    internal DbSet<T> _dbSet;

    public Repository(TokenVaultDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<T>();
    }

    public async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter)
    {
        IQueryable<T> query = _dbSet;
        query = query.Where(filter);
        
        var result = await query.FirstOrDefaultAsync();
        return result;
    }

    public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
    {
        IQueryable<T> query = _dbSet;
        if (filter != null)
        {
            query = query.Where(filter);
        }
        if (includeProperties != null)
        {
            foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(property);
            }
        }
        
        var result = await query.ToListAsync();
        return result;
    }

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public void Remove(T entity)
    {
        _dbSet.Remove(entity);
    }

    public void RemoveRange(IEnumerable<T> entities)
    {
        _dbSet.RemoveRange(entities);
    }
}