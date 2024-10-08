using TokenVault.Domain.Entities;

namespace TokenVault.Application.Common.Interfaces.Persistence;

public interface ITransactionRepository : IRepository<Transaction>
{
    Task<List<Transaction>> GetTransactionsAsync();
    Task<List<Transaction>> GetTransactionsByUserIdAsync(Guid userId);
    Task<Transaction?> GetTransactionByIdAsync(Guid id);
    Task<List<Transaction>> GetTransactionsByPortfolioIdAsync(Guid portfolioId);
    Task<List<Transaction>> GetTransactionsByCryptocurrencyIdAsync(Guid cryptocurrencyId);
}