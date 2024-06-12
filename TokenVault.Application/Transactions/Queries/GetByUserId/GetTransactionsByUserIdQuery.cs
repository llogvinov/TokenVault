using MediatR;
using TokenVault.Application.Transactions.Common;

namespace TokenVault.Application.Transactions.Queries.GetByUserId;

public record GetTransactionsByUserIdQuery(
    Guid UserId
) : IRequest<TransactionsResult>;