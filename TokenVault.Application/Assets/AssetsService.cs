using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Domain.Entities;

namespace TokenVault.Application.Assets;

public class AssetsService
{
    private readonly IAssetRepository _assetRepository;

    public AssetsService(IAssetRepository assetRepository)
    {
        _assetRepository = assetRepository;
    }

    public void AddAsset(Asset asset)
    {
        _assetRepository.Add(asset);
    }

    public void UpdateAssetInPortfolio(Guid portfolioId, Guid cryptocurrencyId, UpdateAssetDetails assetDetails)
    {
        var asset = _assetRepository.GetAssetInPortfolio(portfolioId, cryptocurrencyId);

        var newAssetDetails = CalculateAssetDetails(asset, assetDetails);

        _assetRepository.Update(portfolioId, cryptocurrencyId, newAssetDetails);
    }

    public void DeleteAsset(Guid portfolioId, Guid cryptocurrencyId)
    {
        _assetRepository.Delete(portfolioId, cryptocurrencyId);
    }

    private UpdateAssetDetails CalculateAssetDetails(Asset asset, UpdateAssetDetails assetDetails)
    {
        var amount = asset.Amount + assetDetails.Amount;
        var invested = asset.Invested + assetDetails.Invested;

        return new UpdateAssetDetails(amount, invested / amount, invested);
    }
}