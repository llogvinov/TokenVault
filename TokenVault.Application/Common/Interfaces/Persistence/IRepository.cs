using System.Linq.Expressions;

namespace TokenVault.Application.Common.Interfaces.Persistence;

public interface IRepository<T> where T : class
{
    Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter);
    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);
    Task AddAsync(T entity);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);
}