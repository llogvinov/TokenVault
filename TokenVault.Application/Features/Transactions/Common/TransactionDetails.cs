namespace TokenVault.Application.Features.Transactions.Common;

public record TransactionDetails(
    double Amount,
    double PricePerToken,
    double TotalPrice);