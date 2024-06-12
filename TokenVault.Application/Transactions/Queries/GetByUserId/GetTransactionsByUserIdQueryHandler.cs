using MediatR;
using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Application.Transactions.Common;

namespace TokenVault.Application.Transactions.Queries.GetByUserId;

public class GetTransactionsByUserIdQueryHandler : IRequestHandler<GetTransactionsByUserIdQuery, TransactionsResult>
{
    private readonly ITransactionRepository _transactionRepository;

    public GetTransactionsByUserIdQueryHandler(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task<TransactionsResult> Handle(GetTransactionsByUserIdQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        var transactions = _transactionRepository.GetTransactionsByUserId(query.UserId);

        var transactionsResult = new TransactionsResult(transactions);
        return transactionsResult;
    }
}