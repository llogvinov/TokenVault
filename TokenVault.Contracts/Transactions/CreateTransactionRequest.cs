namespace TokenVault.Contracts.Transactions;

public record CreateTransactionRequest(
    Guid CryptocurrencyId,
    int TransactionType,
    double Amount,
    double PricePerToken,
    double TotalPrice,
    DateTime CreateDate);