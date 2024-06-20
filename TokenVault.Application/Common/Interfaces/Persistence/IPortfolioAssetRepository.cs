using TokenVault.Application.Features.PortfolioAssets.Common;
using TokenVault.Domain.Entities;

namespace TokenVault.Application.Common.Interfaces.Persistence;

public interface IPortfolioAssetRepository
{
    Task CreateAsync(PortfolioAsset portfolioAsset);

    Task<PortfolioAsset> UpdateAsync(Guid cryptocurrencyId, Guid portfolioId, UpdatePortfolioAssetDetails updatePortfolioAssetDetails);

    Task DeleteAsync(Guid cryptocurrencyId, Guid portfolioId);

    Task<PortfolioAsset?> GetPortfolioAssetAsync(Guid cryptocurrencyId, Guid portfolioId);

    Task<IEnumerable<PortfolioAsset>> GetPortfolioAssetsAsync(Guid portfolioId);
}