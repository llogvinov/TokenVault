namespace TokenVault.Application.Transactions.Common;

public record TransactionDetails(
    double Amount,
    double PricePerToken,
    double TotalPrice
);