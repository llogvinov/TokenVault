namespace TokenVault.Application.Transactions.Common;

public record SingleTransactionResult(
    Guid Id,
    Guid UserId,
    string AssetSymbol,
    double Quantity
);