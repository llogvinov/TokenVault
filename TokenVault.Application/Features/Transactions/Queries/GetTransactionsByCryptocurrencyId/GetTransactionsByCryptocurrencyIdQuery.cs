using MediatR;
using TokenVault.Application.Features.Transactions.Common;

namespace TokenVault.Application.Transactions.Queries.GetByCryptocurrencyId;

public record GetTransactionsByCryptocurrencyIdQuery(
    Guid CryptocurrencyId) : IRequest<TransactionsResult>;