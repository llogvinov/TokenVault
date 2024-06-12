using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Domain.Entities;

public class TransactionRepository : ITransactionRepository
{
    private static readonly List<Transaction> _transactions = new();

    public void Add(Transaction transaction)
    {
        _transactions.Add(transaction);
    }

    public Transaction? GetTransactionById(Guid id)
    {
        return _transactions.SingleOrDefault(t => t.Id == id);
    }
    
    public IEnumerable<Transaction> GetTransactionsByUserId(Guid userId)
    {
        return _transactions.Where(t => t.UserId == userId);
    }

    public IEnumerable<Transaction> GetTransactionsByPortfolioId(Guid portfolioId)
    {
        return _transactions.Where(t => t.PortfolioId == portfolioId);
    }
    
    public IEnumerable<Transaction> GetTransactionsBySymbol(string symbol)
    {
        return _transactions.Where(t => t.AssetSymbol == symbol);
    }
}