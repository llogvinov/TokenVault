namespace TokenVault.Application.Features.PortfolioAssets.Common;

public record PortfolioAssetResult(
    Guid CryptocurrencyId,
    Guid PortfolioId,
    double Amount,
    double AveragePrice,
    double Invested);