using MediatR;
using TokenVault.Domain.Entities;

public record CreateTransactionCommand(
    Guid PortfolioId,
    string AssetSymbol,
    double? Quantity,
    double? Price,
    double? Total,
    DateTime? Date
) : IRequest<Transaction>;