using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
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

    public async Task<List<T>> QueryAsync(string query)
    {
        using (IDbConnection db = new SqlConnection(DbConnection.ConnectionString))
        {
            var result = await db.QueryAsync<T>(query);
            return result.ToList();
        }
    }

    public async Task<T?> QueryFirstOrDefaultAsync(string query)
    {
        using (IDbConnection db = new SqlConnection(DbConnection.ConnectionString))
        {
            var result = await db.QueryFirstOrDefaultAsync<T>(query);
            return result;
        }
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