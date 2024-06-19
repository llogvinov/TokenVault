using MediatR;
using TokenVault.Application.Features.Transactions.Common;

namespace TokenVault.Application.Features.Transactions.Queries.GetTransactionsByPortfolioId;

public record GetTransactionsByPortfolioIdQuery(
    Guid PortfolioId) : IRequest<TransactionsResult>;