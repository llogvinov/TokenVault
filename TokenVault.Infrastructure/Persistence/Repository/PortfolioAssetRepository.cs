using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Application.Features.PortfolioAssets.Common;
using TokenVault.Domain.Entities;

namespace TokenVault.Infrastructure.Persistence.Repository;

public class PortfolioAssetRepository : Repository<PortfolioAsset>, IPortfolioAssetRepository
{
    public PortfolioAssetRepository(TokenVaultDbContext dbContext) 
        : base(dbContext) { }

    public async Task<PortfolioAsset> UpdateAsync(
        Guid cryptocurrencyId,
        Guid portfolioId,
        UpdatedAssetDetails updatePortfolioAssetDetails)
    {
        var portfolioAsset = await GetFirstOrDefaultAsync(
            a => a.CryptocurrencyId == cryptocurrencyId && a.PortfolioId ==  portfolioId);
        if (portfolioAsset is null)
        {
            throw new ArgumentNullException(nameof(portfolioAsset),
                $"Portfolio asset with given cryptocurrency id: {cryptocurrencyId}"
                + " does not exist in given portfolio with id: {portfolioId}");
        }
        
        portfolioAsset.Holdings = updatePortfolioAssetDetails.Holdings;
        portfolioAsset.AveragePrice = updatePortfolioAssetDetails.PricePerToken;
        portfolioAsset.Invested = updatePortfolioAssetDetails.TotalPrice;

        return portfolioAsset;
    }
}