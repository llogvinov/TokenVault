namespace TokenVault.Application.Transactions.Common;

public record SingleTransactionResult(
    Guid Id,
    Guid UserId,
    Guid PortfolioId,
    string AssetSymbol,
    double Quantity
);