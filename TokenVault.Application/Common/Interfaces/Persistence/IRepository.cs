namespace TokenVault.Application.Common.Interfaces.Persistence;

public interface IRepository<T> where T : class
{
    Task<List<T>> QueryAsync(string query);
    Task<T?> QueryFirstOrDefaultAsync(string query);
    Task AddAsync(T entity);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);
}