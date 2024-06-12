using MediatR;
using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Application.Transactions.Queries.GetByPortfolioId;
using TokenVault.Application.Transactions.Queries.GetByUserId;
using TokenVault.Domain.Entities;

namespace TokenVault.Application.Transactions;

public class TransactionsService
{
    private readonly ISender _mediator;
    private readonly ITransactionRepository _transactionRepository;

    public TransactionsService(ITransactionRepository transactionRepository, ISender mediator)
    {
        _transactionRepository = transactionRepository;
        _mediator = mediator;
    }

    public async Task<IEnumerable<Transaction>> GetTransactionsByUserId(Guid userId)
    {
        var query = new GetTransactionsByUserIdQuery(userId);
        var transactionsResult = await _mediator.Send(query);

        return transactionsResult.Transactions;
    }

    public async Task<IEnumerable<Transaction>> GetTransactionsByPortfolioId(Guid portfolioId)
    {
        var query = new GetTransactionsByPortfolioIdQuery(portfolioId);
        var transactionsResult = await _mediator.Send(query);

        return transactionsResult.Transactions;
    }
}