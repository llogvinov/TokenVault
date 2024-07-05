using MediatR;
using TokenVault.Application.Features.Transactions.Common;

namespace TokenVault.Application.Features.Transactions.Commands.CreateTransaction;

public record CreateTransactionCommand(
    Guid PortfolioId,
    Guid CryptocurrencyId,
    int TransactionType,
    double Amount,
    double PricePerToken,
    double TotalPrice,
    DateTime CreateDate) : IRequest<SingleTransactionResult>;