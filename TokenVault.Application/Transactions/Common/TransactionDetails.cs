namespace TokenVault.Application.Transactions.Common;

public record TransactionDetails(
    double Quantity,
    double Price,
    double Total
);