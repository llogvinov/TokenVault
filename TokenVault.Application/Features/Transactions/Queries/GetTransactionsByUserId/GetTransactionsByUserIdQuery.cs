using MediatR;
using TokenVault.Application.Features.Transactions.Common;

namespace TokenVault.Application.Features.Transactions.Queries.GetTransactionsByUserId;

public record GetTransactionsByUserIdQuery(
    Guid UserId) : IRequest<TransactionsResult>;