namespace TokenVault.Application.Features.Transactions.Common;

public record SingleTransactionResult(
    Guid Id,
    Guid UserId,
    Guid PortfolioId,
    Guid CryptocurrencyId,
    int TransactionType,
    string AssetSymbol,
    double Amount,
    double PricePerToken,
    double TotalPrice,
    DateTime CreateDate);