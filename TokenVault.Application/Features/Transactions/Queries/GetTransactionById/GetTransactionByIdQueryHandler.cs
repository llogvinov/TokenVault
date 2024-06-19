using MediatR;
using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Domain.Entities;

namespace TokenVault.Application.Features.Transactions.Queries.GetTransactionById;

public class GetTransactionByIdQueryHandler :
    IRequestHandler<GetTransactionByIdQuery, Transaction>
{
    private readonly ITransactionRepository _transactionRepository;

    public GetTransactionByIdQueryHandler(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task<Transaction> Handle(
        GetTransactionByIdQuery query,
        CancellationToken cancellationToken)
    {
        var transaction = await _transactionRepository.GetTransactionByIdAsync(query.transactionId);
        if (transaction is null)
        {
            throw new ArgumentNullException(nameof(transaction),
                $"Transaction with given id: {query.transactionId} does not exist");
        }
        
        return transaction;
    }
}