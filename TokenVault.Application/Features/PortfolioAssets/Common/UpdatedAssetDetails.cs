namespace TokenVault.Application.Features.PortfolioAssets.Common;

public record UpdatedAssetDetails(
    double Amount,
    double PricePerToken,
    double TotalPrice);