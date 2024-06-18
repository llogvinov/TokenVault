using MediatR;
using TokenVault.Application.Transactions.Common;

namespace TokenVault.Application.Transactions.Commands.Create;

public record CreateTransactionCommand(
    Guid UserId,
    Guid PortfolioId,
    Guid CryptocurrencyId,
    double? Amount,
    double? PricePerToken,
    double? TotalPrice,
    DateTime? CreateDate
) : IRequest<SingleTransactionResult>;