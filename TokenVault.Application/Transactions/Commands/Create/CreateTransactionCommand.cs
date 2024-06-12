using MediatR;
using TokenVault.Domain.Entities;

namespace TokenVault.Application.Transactions.Commands.Create;

public record CreateTransactionCommand(
    Guid PortfolioId,
    string AssetSymbol,
    double? Quantity,
    double? Price,
    double? Total,
    DateTime? Date
) : IRequest<Transaction>;