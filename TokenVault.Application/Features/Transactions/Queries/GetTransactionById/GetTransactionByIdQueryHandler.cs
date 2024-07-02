using MediatR;
using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Domain.Entities;

namespace TokenVault.Application.Features.Transactions.Queries.GetTransactionById;

public class GetTransactionByIdQueryHandler :
    IRequestHandler<GetTransactionByIdQuery, Transaction>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetTransactionByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Transaction> Handle(
        GetTransactionByIdQuery query,
        CancellationToken cancellationToken)
    {
        var transaction = await _unitOfWork.Transaction.GetFirstOrDefaultAsync(
            t => t.Id == query.transactionId);
        if (transaction is null)
        {
            throw new ArgumentNullException(nameof(transaction),
                $"Transaction with given id: {query.transactionId} does not exist");
        }
        
        return transaction;
    }
}