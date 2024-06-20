namespace TokenVault.Application.Features.PortfolioAssets.Common;

public record UpdatePortfolioAssetDetails(
    double Amount,
    double AveragePrice,
    double Invested);