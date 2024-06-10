namespace TokenVault.Contracts.Transactions;

public record CreateTransactionRequest(
    string AssetSymbol,
    double? Quantity,
    double? Price,
    double? Total,
    DateTime? Date
);