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
        var portfolioAssetFromDb = await GetPortfolioAssetAsync(cryptocurrencyId, portfolioId);
        if (portfolioAssetFromDb is null)
        {
            throw new ArgumentNullException(nameof(portfolioAssetFromDb),
                $"Portfolio asset with given cryptocurrency id: {cryptocurrencyId} "
                + $"does not exist in given portfolio with id: {portfolioId}");
        }

        portfolioAssetFromDb.Holdings = updatePortfolioAssetDetails.Holdings;
        portfolioAssetFromDb.AveragePrice = updatePortfolioAssetDetails.PricePerToken;
        portfolioAssetFromDb.Invested = updatePortfolioAssetDetails.TotalPrice;
        return portfolioAssetFromDb;
    }

    public async Task<PortfolioAsset?> GetPortfolioAssetAsync(Guid cryptocurrencyId, Guid portfolioId)
    {
        string query = $"SELECT * FROM PortfolioAssets " +
                    $"WHERE CryptocurrencyId = {cryptocurrencyId} " +
                    $"AND PortfolioId == {portfolioId}";
        return await QueryFirstOrDefaultAsync(query);
    }
}