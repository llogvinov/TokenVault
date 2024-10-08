using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Domain.Entities;

namespace TokenVault.Infrastructure.Persistence.Repository;

public class TransactionRepository : Repository<Transaction>, ITransactionRepository
{
    public TransactionRepository(TokenVaultDbContext dbContext)
        : base(dbContext) { }

    public async Task<List<Transaction>> GetTransactionsAsync()
    {
        using (IDbConnection db = new SqlConnection(DbConnection.ConnectionString))
        {
            var query = "SELECT * FROM Transactions";
            var result = await db.QueryAsync<Transaction>(query);
            return result.ToList();
        }
    }

    public async Task<List<Transaction>> GetTransactionsByUserIdAsync(Guid userId)
    {
        using (IDbConnection db = new SqlConnection(DbConnection.ConnectionString))
        {
            var query = $"SELECT * FROM Transactions WHERE UserId = '{userId}'";
            var result = await db.QueryAsync<Transaction>(query);
            return result.ToList();
        }
    }

    public async Task<List<Transaction>> GetTransactionsByPortfolioIdAsync(Guid portfolioId)
    {
        using (IDbConnection db = new SqlConnection(DbConnection.ConnectionString))
        {
            var query = $"SELECT * FROM Transactions WHERE PortfolioId = '{portfolioId}'";
            var result = await db.QueryAsync<Transaction>(query);
            return result.ToList();
        }
    }

    public async Task<List<Transaction>> GetTransactionsByCryptocurrencyIdAsync(Guid cryptocurrencyId)
    {
        using (IDbConnection db = new SqlConnection(DbConnection.ConnectionString))
        {
            var query = $"SELECT * FROM Transactions WHERE CryptocurrencyId = '{cryptocurrencyId}'";
            var result = await db.QueryAsync<Transaction>(query);
            return result.ToList();
        }
    }

    public async Task<Transaction?> GetTransactionByIdAsync(Guid id)
    {
        using (IDbConnection db = new SqlConnection(DbConnection.ConnectionString))
        {
            var query = $"SELECT * FROM Transactions WHERE Id = '{id}'";
            var result = await db.QueryFirstOrDefaultAsync<Transaction>(query);
            return result;
        }
    }
}