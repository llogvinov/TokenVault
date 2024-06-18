using MediatR;
using TokenVault.Application.Transactions.Common;

namespace TokenVault.Application.Transactions.Queries.GetByCryptocurrencyId;

public record GetTransactionsByCryptocurrencyIdQuery(
    Guid CryptocurrencyId
) : IRequest<TransactionsResult>;