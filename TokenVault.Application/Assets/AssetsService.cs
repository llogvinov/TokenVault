using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Application.Features.Transactions.Common;
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
        var asset = _assetRepository.GetAssetInPortfolio(
            transactionResult.PortfolioId,
            transactionResult.CryptocurrencyId);

        if (asset is null)
        {
            var newAsset = GetNewAssetFromTransaction(transactionResult);
            _assetRepository.Add(newAsset);
        }
        else
        {
            var updateAssetDetails = GetUpdatedAssetDetails(asset, transactionResult);
            _assetRepository.Update(
                transactionResult.PortfolioId,
                transactionResult.CryptocurrencyId,
                updateAssetDetails);
        }
    }

    private Asset GetNewAssetFromTransaction(SingleTransactionResult transactionResult)
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

    private UpdateAssetDetails GetUpdatedAssetDetails(Asset asset, SingleTransactionResult transactionResult)
    {
        var amount = asset.Amount + transactionResult.Amount;
        var invested = asset.Invested + transactionResult.TotalPrice;
        var averagePrice = invested / amount;

        var updateAssetDetails = new UpdateAssetDetails(amount, averagePrice, invested);
        return updateAssetDetails;
    }

    public void DeleteAsset(Guid portfolioId, Guid cryptocurrencyId)
    {
        _assetRepository.Delete(portfolioId, cryptocurrencyId);
    }
}