using MediatR;
using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Application.Transactions.Common;

namespace TokenVault.Application.Transactions.Queries.GetByCryptocurrencyId;

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
        await Task.CompletedTask;

        var transactions = _transactionRepository.GetTransactionsByCryptocurrencyId(query.CryptocurrencyId);

        var result = new TransactionsResult(transactions);
        return result;
    }
}