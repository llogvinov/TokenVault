using MediatR;
using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Application.Features.PortfolioAssets.Common;
using TokenVault.Domain.Entities;

namespace TokenVault.Application.Features.PortfolioAssets.Commands.CreatePortfolioAsset;

public class CreatePortfolioAssetCommandHandler :
    IRequestHandler<CreatePortfolioAssetCommand, PortfolioAssetResult>
{
    private readonly IPortfolioAssetRepository _portfolioAssetRepository;

    public CreatePortfolioAssetCommandHandler(IPortfolioAssetRepository portfolioAssetRepository)
    {
        _portfolioAssetRepository = portfolioAssetRepository;
    }

    public async Task<PortfolioAssetResult> Handle(
        CreatePortfolioAssetCommand command,
        CancellationToken cancellationToken)
    {
        var portfolioAsset = new PortfolioAsset
        {
            CryptocurrencyId = command.CryptocurrencyId,
            PortfolioId = command.PortfolioId,
            Amount = command.Amount,
            AveragePrice = command.AveragePrice,
            Invested = command.Invested
        };
        await _portfolioAssetRepository.CreateAsync(portfolioAsset);

        var portfolioAssetResult = new PortfolioAssetResult(
            portfolioAsset.CryptocurrencyId,
            portfolioAsset.PortfolioId,
            portfolioAsset.Amount,
            portfolioAsset.AveragePrice,
            portfolioAsset.Invested);
        return portfolioAssetResult;
    }
}