using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Domain.Entities;

namespace TokenVault.Application.Transactions;

public class TransactionsService
{
    private readonly ITransactionRepository _transactionRepository;

    public TransactionsService(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public IEnumerable<Transaction> GetTransactions(Guid portfolioId)
    {
        return _transactionRepository.GetTransactionsByPortfolioId(portfolioId);
    }
}