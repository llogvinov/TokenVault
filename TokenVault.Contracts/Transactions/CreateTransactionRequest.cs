namespace TokenVault.Contracts.Transactions;

public record CreateTransactionRequest(
    Guid CryptocurrencyId,
    double Amount,
    double PricePerToken,
    double TotalPrice,
    DateTime? CreateDate);