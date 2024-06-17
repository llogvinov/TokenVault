using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Application.Transactions.Common;
using TokenVault.Domain.Entities;

namespace TokenVault.Application.Assets;

public class AssetsService
{
    private readonly IAssetRepository _assetRepository;

    public AssetsService(IAssetRepository assetRepository)
    {
        _assetRepository = assetRepository;
    }

    public void UpdateAssetInPortfolio(SingleTransactionResult transactionResult)
    {
        var asset = _assetRepository.GetAssetInPortfolio(transactionResult.PortfolioId, transactionResult.CryptocurrencyId);
        if (asset is null)
        {
            var newAsset = GetNewAssetFromTransaction(transactionResult);
            _assetRepository.Add(newAsset);
        }
        else
        {
            var updateAssetDetails = GetUpdatedAssetDetails(transactionResult, asset);
            _assetRepository.Update(
                transactionResult.PortfolioId,
                transactionResult.CryptocurrencyId,
                updateAssetDetails);
        }
    }

    private static UpdateAssetDetails GetUpdatedAssetDetails(SingleTransactionResult transactionResult, Asset asset)
    {
        var amount = asset.Amount + transactionResult.Amount;
        var invested = asset.Invested + transactionResult.TotalPrice;
        var averagePrice = invested / amount;

        var updateAssetDetails = new UpdateAssetDetails(amount, averagePrice, invested);
        return updateAssetDetails;
    }

    private static Asset GetNewAssetFromTransaction(SingleTransactionResult transactionResult)
    {
        return new Asset
        {
            CryptocurrencyId = transactionResult.CryptocurrencyId,
            PortfolioId = transactionResult.PortfolioId,
            Amount = transactionResult.Amount,
            AveragePrice = transactionResult.PricePerToken,
            Invested = transactionResult.TotalPrice
        };
    }

    public void DeleteAsset(Guid portfolioId, Guid cryptocurrencyId)
    {
        _assetRepository.Delete(portfolioId, cryptocurrencyId);
    }
}