using MediatR;
using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Application.Transactions.Common;

namespace TokenVault.Application.Transactions.Queries.GetByPortfolioId;

public class GetTransactionsByPortfolioIdQueryHandler : IRequestHandler<GetTransactionsByPortfolioIdQuery, TransactionsResult>
{
    private readonly ITransactionRepository _transactionRepository;

    public GetTransactionsByPortfolioIdQueryHandler(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task<TransactionsResult> Handle(GetTransactionsByPortfolioIdQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        var transactions = _transactionRepository.GetTransactionsByPortfolioId(query.PortfolioId);

        var transactionsResult = new TransactionsResult(transactions);
        return transactionsResult;
    }
}