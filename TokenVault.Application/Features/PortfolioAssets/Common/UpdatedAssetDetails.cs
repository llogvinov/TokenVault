namespace TokenVault.Application.Features.PortfolioAssets.Common;

public record UpdatedAssetDetails(
    double Holdings,
    double PricePerToken,
    double TotalPrice);