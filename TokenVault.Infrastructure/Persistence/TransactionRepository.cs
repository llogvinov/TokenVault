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

    public IEnumerable<Transaction> GetTransactionsBySymbol(string symbol)
    {
        return _transactions.Where(t => t.AssetSymbol == symbol);
    }
}