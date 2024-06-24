using MediatR;
using TokenVault.Application.Features.Transactions.Common;

namespace TokenVault.Application.Features.Transactions.Commands.CreateTransaction;

public record CreateTransactionCommand(
    Guid UserId,
    Guid PortfolioId,
    Guid CryptocurrencyId,
    double Amount,
    double PricePerToken,
    double TotalPrice,
    DateTime? CreateDate) : IRequest<SingleTransactionResult>;