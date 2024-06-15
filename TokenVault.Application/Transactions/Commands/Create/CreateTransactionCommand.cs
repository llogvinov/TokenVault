using MediatR;
using TokenVault.Application.Transactions.Common;

namespace TokenVault.Application.Transactions.Commands.Create;

public record CreateTransactionCommand(
    Guid UserId,
    Guid PortfolioId,
    string AssetSymbol,
    double? Quantity,
    double? Price,
    double? Total,
    DateTime? Date
) : IRequest<SingleTransactionResult>;