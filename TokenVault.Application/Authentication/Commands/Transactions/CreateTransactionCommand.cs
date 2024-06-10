using MediatR;
using TokenVault.Domain.Entities;

public record CreateTransactionCommand(
    string AssetSymbol,
    double? Quantity,
    double? Price,
    double? Total,
    DateTime? Date
) : IRequest<Transaction>;