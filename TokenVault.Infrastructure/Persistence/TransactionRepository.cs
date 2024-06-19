using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Domain.Entities;

public class TransactionRepository : ITransactionRepository
{
    private static readonly List<Transaction> _transactions = new();

    public async Task CreateAsync(Transaction transaction)
    {
        await Task.CompletedTask;

        _transactions.Add(transaction);
    }

    public async Task DeleteAsync(Guid id)
    {
        await Task.CompletedTask;

        var transaction = _transactions.FirstOrDefault(t => t.Id == id);
        if (transaction is not null)
        {
            _transactions.Remove(transaction);
        }
    }

    public async Task DeleteByPortfolioIdAsync(Guid portfolioId)
    {
        await Task.CompletedTask;

        var transactions = _transactions.Where(t => t.PortfolioId == portfolioId);
        foreach (var transaction in transactions)
        {
            _transactions.Remove(transaction);
        }
    }

    public async Task<Transaction?> GetTransactionByIdAsync(Guid id)
    {
        await Task.CompletedTask;
        
        return _transactions.SingleOrDefault(t => t.Id == id);
    }
    
    public async Task<IEnumerable<Transaction>> GetTransactionsByUserIdAsync(Guid userId)
    {
        await Task.CompletedTask;

        return _transactions.Where(t => t.UserId == userId);
    }

    public async Task<IEnumerable<Transaction>> GetTransactionsByPortfolioIdAsync(Guid portfolioId)
    {
        await Task.CompletedTask;

        return _transactions.Where(t => t.PortfolioId == portfolioId);
    }
    
    public async Task<IEnumerable<Transaction>> GetTransactionsByCryptocurrencyIdAsync(Guid cryptocurrencyId)
    {
        await Task.CompletedTask;

        return _transactions.Where(t => t.CryptocurrencyId == cryptocurrencyId);
    }
}