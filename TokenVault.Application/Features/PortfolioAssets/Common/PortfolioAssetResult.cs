namespace TokenVault.Application.Features.PortfolioAssets.Common;

public record PortfolioAssetResult(
    Guid CryptocurrencyId,
    Guid PortfolioId,
    double Holdings,
    double AveragePrice,
    double Invested);