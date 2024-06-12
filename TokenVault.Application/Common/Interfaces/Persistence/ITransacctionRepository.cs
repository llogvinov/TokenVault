using TokenVault.Domain.Entities;

namespace TokenVault.Application.Common.Interfaces.Persistence;

public interface ITransactionRepository
{
    void Add(Transaction transaction);

    Transaction? GetTransactionById(Guid id);

    IEnumerable<Transaction> GetTransactionsByUserId(Guid userId);

    IEnumerable<Transaction> GetTransactionsByPortfolioId(Guid portfolioId);

    IEnumerable<Transaction> GetTransactionsBySymbol(string symbol);
}