using MediatR;
using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Application.Features.Transactions.Common;

namespace TokenVault.Application.Features.Transactions.Queries.GetTransactionsByUserId;

public class GetTransactionsByUserIdQueryHandler : 
    IRequestHandler<GetTransactionsByUserIdQuery, TransactionsResult>
{
    private readonly ITransactionRepository _transactionRepository;

    public GetTransactionsByUserIdQueryHandler(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task<TransactionsResult> Handle(
        GetTransactionsByUserIdQuery query,
        CancellationToken cancellationToken)
    {
        var transactions = await _transactionRepository.GetTransactionsByUserIdAsync(query.UserId);

        var transactionsResult = new TransactionsResult(transactions);
        return transactionsResult;
    }
}