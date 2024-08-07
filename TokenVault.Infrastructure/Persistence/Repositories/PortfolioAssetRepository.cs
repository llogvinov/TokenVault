using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Application.Features.PortfolioAssets.Common;
using TokenVault.Domain.Entities;

namespace TokenVault.Infrastructure.Persistence;

public class PortfolioAssetRepository
{
    private static readonly List<PortfolioAsset> _portfolioAssets = new();
    
    public async Task CreateAsync(PortfolioAsset portfolioAsset)
    {
        await Task.CompletedTask;

        _portfolioAssets.Add(portfolioAsset);
    }

    public async Task<PortfolioAsset> UpdateAsync(
        Guid cryptocurrencyId,
        Guid portfolioId,
        UpdatedAssetDetails updatePortfolioAssetDetails)
    {
        await Task.CompletedTask;

        var portfolioAsset = await GetPortfolioAssetAsync(cryptocurrencyId, portfolioId);
        if (portfolioAsset is null)
        {
            throw new ArgumentNullException(nameof(portfolioAsset),
                $"Portfolio asset with given id: {cryptocurrencyId} does not exist");
        }
        
        portfolioAsset.Holdings = updatePortfolioAssetDetails.Holdings;
        portfolioAsset.AveragePrice = updatePortfolioAssetDetails.PricePerToken;
        portfolioAsset.Invested = updatePortfolioAssetDetails.TotalPrice;

        return portfolioAsset;
    }

    public async Task DeleteAsync(Guid cryptocurrencyId, Guid portfolioId)
    {
        await Task.CompletedTask;

        var portfolioAsset = await GetPortfolioAssetAsync(cryptocurrencyId, portfolioId);
        if (portfolioAsset is not null)
        {
            _portfolioAssets.Remove(portfolioAsset);
        }
    }

    public async Task<PortfolioAsset?> GetPortfolioAssetAsync(Guid cryptocurrencyId, Guid portfolioId)
    {
        await Task.CompletedTask;

        var portfolioAsset = _portfolioAssets.FirstOrDefault(a => 
            a.CryptocurrencyId == cryptocurrencyId && a.PortfolioId == portfolioId);
        
        return portfolioAsset;
    }

    public async Task<IEnumerable<PortfolioAsset>> GetPortfolioAssetsAsync(Guid portfolioId)
    {
        await Task.CompletedTask;

        var portfolioAssets = _portfolioAssets.Where(a => a.PortfolioId == portfolioId);
        
        if (portfolioAssets is null)
        {
            throw new ArgumentNullException(nameof(portfolioAssets),
                $"Portfolio asset with given id: {portfolioId} does not exist");
        }
        
        return portfolioAssets;
    }
}