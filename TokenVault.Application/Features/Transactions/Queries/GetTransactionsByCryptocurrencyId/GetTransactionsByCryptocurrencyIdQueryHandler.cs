using MediatR;
using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Application.Features.Transactions.Common;
using TokenVault.Application.Transactions.Queries.GetByCryptocurrencyId;

namespace TokenVault.Application.Features.Transactions.Queries.GetTransactionsByCryptocurrencyId;

public class GetTransactionsByCryptocurrencyIdQueryHandler :
    IRequestHandler<GetTransactionsByCryptocurrencyIdQuery, TransactionsResult>
{
    private readonly ITransactionRepository _transactionRepository;

    public GetTransactionsByCryptocurrencyIdQueryHandler(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task<TransactionsResult> Handle(
        GetTransactionsByCryptocurrencyIdQuery query,
        CancellationToken cancellationToken)
    {
        var transactions = await _transactionRepository.GetTransactionsByCryptocurrencyIdAsync(query.CryptocurrencyId);

        var result = new TransactionsResult(transactions);
        return result;
    }
}