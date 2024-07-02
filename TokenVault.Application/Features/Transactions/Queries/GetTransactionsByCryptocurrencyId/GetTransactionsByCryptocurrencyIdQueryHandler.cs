using MediatR;
using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Application.Features.Transactions.Common;
using TokenVault.Application.Transactions.Queries.GetByCryptocurrencyId;

namespace TokenVault.Application.Features.Transactions.Queries.GetTransactionsByCryptocurrencyId;

public class GetTransactionsByCryptocurrencyIdQueryHandler :
    IRequestHandler<GetTransactionsByCryptocurrencyIdQuery, TransactionsResult>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetTransactionsByCryptocurrencyIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TransactionsResult> Handle(
        GetTransactionsByCryptocurrencyIdQuery query,
        CancellationToken cancellationToken)
    {
        var transactions = await _unitOfWork.Transaction.GetAllAsync(
            t => t.CryptocurrencyId == query.CryptocurrencyId);

        var result = new TransactionsResult(transactions);
        return result;
    }
}