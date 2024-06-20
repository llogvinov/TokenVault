using MediatR;
using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Application.Features.PortfolioAssets.Common;
using TokenVault.Application.Features.Transactions.Common;
using TokenVault.Domain.Entities;

namespace TokenVault.Application.Features.PortfolioAssets.Commands.UpdatePortfolioAsset;

public class UpdatePortfolioAssetCommandHandler :
    IRequestHandler<UpdatePortfolioAssetCommand, PortfolioAssetResult>
{
    private readonly IPortfolioAssetRepository _portfolioAssetRepository;

    public UpdatePortfolioAssetCommandHandler(IPortfolioAssetRepository portfolioAssetRepository)
    {
        _portfolioAssetRepository = portfolioAssetRepository;
    }

    public async Task<PortfolioAssetResult> Handle(
        UpdatePortfolioAssetCommand command,
        CancellationToken cancellationToken)
    {
        var portfolioAsset = await _portfolioAssetRepository.GetPortfolioAssetAsync(
            command.CryptocurrencyId, command.PortfolioId);
        var updatePortfolioAssetDetails = GetUpdatedAssetDetails(
            portfolioAsset, command.TransactionResult);

        var updatedPortfolioAsset = await _portfolioAssetRepository.UpdateAsync(
            command.CryptocurrencyId,
            command.PortfolioId,
            updatePortfolioAssetDetails);

        var portfolioAssetResult = new PortfolioAssetResult(
            updatedPortfolioAsset.CryptocurrencyId,
            updatedPortfolioAsset.PortfolioId,
            updatedPortfolioAsset.Amount,
            updatedPortfolioAsset.AveragePrice,
            updatedPortfolioAsset.Invested);
        return portfolioAssetResult;
    }

    private UpdatePortfolioAssetDetails GetUpdatedAssetDetails(
        PortfolioAsset asset,
        SingleTransactionResult transactionResult)
    {
        var amount = asset.Amount + transactionResult.Amount;
        var invested = asset.Invested + transactionResult.TotalPrice;
        var averagePrice = invested / amount;

        var updateAssetDetails = new UpdatePortfolioAssetDetails(amount, averagePrice, invested);
        return updateAssetDetails;
    }
}