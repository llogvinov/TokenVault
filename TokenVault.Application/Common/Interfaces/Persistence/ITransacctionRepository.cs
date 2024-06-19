using TokenVault.Domain.Entities;

namespace TokenVault.Application.Common.Interfaces.Persistence;

public interface ITransactionRepository
{
    Task CreateAsync(Transaction transaction);

    Task DeleteAsync(Guid id);

    Task DeleteByPortfolioIdAsync(Guid portfolioId);

    Task<Transaction?> GetTransactionByIdAsync(Guid id);

    Task<IEnumerable<Transaction>> GetTransactionsByUserIdAsync(Guid userId);

    Task<IEnumerable<Transaction>> GetTransactionsByPortfolioIdAsync(Guid portfolioId);

    Task<IEnumerable<Transaction>> GetTransactionsByCryptocurrencyIdAsync(Guid cryptocurrencyId);
}