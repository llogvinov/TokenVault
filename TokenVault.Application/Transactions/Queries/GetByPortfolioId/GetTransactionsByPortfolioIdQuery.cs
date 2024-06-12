using MediatR;
using TokenVault.Application.Transactions.Common;

namespace TokenVault.Application.Transactions.Queries.GetByPortfolioId;

public record GetTransactionsByPortfolioIdQuery(
    Guid PortfolioId
) : IRequest<TransactionsResult>;