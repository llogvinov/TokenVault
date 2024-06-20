using MediatR;
using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Application.Features.PortfolioAssets.Common;

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
        await _portfolioAssetRepository.UpdateAsync();

        var portfolioAssetResult = new PortfolioAssetResult(
            command.CryptocurrencyId,
            command.PortfolioId,
            command.Amount,
            command.AveragePrice,
            command.Invested);
        return portfolioAssetResult;
    }
}