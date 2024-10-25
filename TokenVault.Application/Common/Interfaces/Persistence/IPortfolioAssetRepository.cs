using TokenVault.Application.Features.PortfolioAssets.Common;
using TokenVault.Domain.Entities;

namespace TokenVault.Application.Common.Interfaces.Persistence;

public interface IPortfolioAssetRepository : IRepository<PortfolioAsset>
{
    Task<PortfolioAsset> UpdateAsync(Guid cryptocurrencyId, Guid portfolioId,
        UpdatedAssetDetails updatePortfolioAssetDetails);

    Task<PortfolioAsset?> GetPortfolioAssetAsync(Guid cryptocurrencyId, Guid portfolioId);
}