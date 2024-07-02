namespace TokenVault.Application.Features.PortfolioAssets.Common;

public record UpdatePortfolioAssetDetails(
    int TransactionType,
    double Amount,
    double PricePerToken,
    double TotalPrice);