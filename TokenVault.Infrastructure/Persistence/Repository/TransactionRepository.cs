using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Domain.Entities;

namespace TokenVault.Infrastructure.Persistence.Repository;

public class TransactionRepository : Repository<Transaction>, ITransactionRepository
{
    public TransactionRepository(TokenVaultDbContext dbContext)
        : base(dbContext) { }

    public async Task<List<Transaction>> GetTransactionsAsync()
    {
        string query = "SELECT * FROM Transactions";
        return await QueryAsync(query);
    }

    public async Task<List<Transaction>> GetTransactionsByUserIdAsync(Guid userId)
    {
        var query = $"SELECT * FROM Transactions WHERE UserId = '{userId}'";
        return await QueryAsync(query);
    }

    public async Task<List<Transaction>> GetTransactionsByPortfolioIdAsync(Guid portfolioId)
    {
        var query = $"SELECT * FROM Transactions WHERE PortfolioId = '{portfolioId}'";
        return await QueryAsync(query);
    }

    public async Task<List<Transaction>> GetTransactionsByCryptocurrencyIdAsync(Guid cryptocurrencyId)
    {
        var query = $"SELECT * FROM Transactions WHERE CryptocurrencyId = '{cryptocurrencyId}'";
        return await QueryAsync(query);
    }

    public async Task<Transaction?> GetTransactionByIdAsync(Guid id)
    {
        var query = $"SELECT * FROM Transactions WHERE Id = '{id}'";
        return await QueryFirstOrDefaultAsync(query); ;
    }
}