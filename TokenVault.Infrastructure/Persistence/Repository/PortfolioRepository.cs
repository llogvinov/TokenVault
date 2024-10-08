using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Domain.Entities;

namespace TokenVault.Infrastructure.Persistence.Repository;

public class PortfolioRepository : Repository<Portfolio>, IPortfolioRepository
{
    public PortfolioRepository(TokenVaultDbContext dbContext) 
        : base(dbContext) { }

    public async Task<Portfolio> UpdateAsync(Guid id, string title)
    {
        var portfolioFromDb = await GetFirstOrDefaultAsync(p => p.Id == id);
        if (portfolioFromDb is null)
        {
            throw new ArgumentNullException(nameof(portfolioFromDb),
                $"Portfolio with given id: {id} does not exist");
        }

        portfolioFromDb.Title = title ?? portfolioFromDb.Title;
        
        return portfolioFromDb;
    }

    public async Task<List<Portfolio>> GetPortfoliosAsync()
    {
        using (IDbConnection db = new SqlConnection(DbConnection.ConnectionString))
        {
            var query = "SELECT * FROM Portfolios";
            var result = await db.QueryAsync<Portfolio>(query);
            return result.ToList();
        }
    }

    public async Task<List<Portfolio>> GetPortfoliosByUserIdAsync(Guid userId)
    {
        using (IDbConnection db = new SqlConnection(DbConnection.ConnectionString))
        {
            var query = $"SELECT * FROM Portfolios WHERE UserId = '{userId}'";
            var result = await db.QueryAsync<Portfolio>(query);
            return result.ToList();
        }
    }

    public async Task<Portfolio?> GetPortfolioByIdAsync(Guid id)
    {
        using (IDbConnection db = new SqlConnection(DbConnection.ConnectionString))
        {
            var query = $"SELECT * FROM Portfolios WHERE Id = '{id}'";
            var result = await db.QueryFirstOrDefaultAsync<Portfolio>(query);
            return result;
        }
    }
}