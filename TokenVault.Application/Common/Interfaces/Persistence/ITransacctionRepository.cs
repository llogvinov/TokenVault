using TokenVault.Domain.Entities;

namespace TokenVault.Application.Common.Interfaces.Persistence;

public interface ITransactionRepository
{
    void Add(Transaction transaction);

    Transaction? GetTransactionById(Guid id);

    IEnumerable<Transaction> GetTransactionsBySymbol(string symbol);
}