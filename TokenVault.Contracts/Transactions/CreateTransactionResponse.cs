namespace TokenVault.Contracts.Transactions;

public record CreateTransactionResponse(
    Guid Id,
    Guid UserId,
    Guid PortfolioId,
    Guid CryptocurrencyId,
    string AssetSymbol,
    double Amount,
    double PricePerToken,
    double TotalPrice,
    DateTime CreateDate);