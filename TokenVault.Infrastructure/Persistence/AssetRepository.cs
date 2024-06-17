using TokenVault.Application.Assets;
using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Domain.Entities;

namespace TokenVault.Infrastructure.Persistence;

public class AssetRepository : IAssetRepository
{
    private static readonly List<Asset> _assets = new();

    public void Add(Asset asset)
    {
        _assets.Add(asset);
    }

    public void Update(Guid portfolioId, Guid cryptocurrencyId, UpdateAssetDetails assetDetails)
    {
        var assetFromDb = _assets.FirstOrDefault(a => a.PortfolioId == portfolioId 
            && a.CryptocurrencyId == cryptocurrencyId);

        if (assetFromDb is not null)
        {
            assetFromDb.Amount = assetDetails.Amount;
            assetFromDb.AveragePrice = assetDetails.AveragePrice;
            assetFromDb.Invested = assetDetails.Invested;
        }
    }

    public void Delete(Guid portfolioId, Guid cryptocurrencyId)
    {
        var asset = _assets.FirstOrDefault(a => a.PortfolioId == portfolioId 
            && a.CryptocurrencyId == cryptocurrencyId);
        
        if (asset is not null)
        {
            _assets.Remove(asset);
        }
    }

    public Asset? GetAssetInPortfolio(Guid portfolioId, Guid cryptocurrencyId)
    {
        var asset = _assets.FirstOrDefault(a => a.PortfolioId == portfolioId 
            && a.CryptocurrencyId == cryptocurrencyId);

        return asset;
    }

    public void DeleteAllInPortfolio(Guid portfolioId)
    {
        var assets = _assets.Where(a => a.PortfolioId == portfolioId);
        if (assets is not null && assets.Any())
        {
            foreach (var asset in assets)
            {
                _assets.Remove(asset);
            }
        }
    }
}