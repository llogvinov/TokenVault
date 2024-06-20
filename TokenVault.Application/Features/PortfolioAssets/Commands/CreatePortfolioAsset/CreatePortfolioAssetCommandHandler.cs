using MediatR;
using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Application.Features.PortfolioAssets.Common;

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
        await _portfolioAssetRepository.CreateAsync();

        var portfolioAssetResult = new PortfolioAssetResult(
            command.CryptocurrencyId,
            command.PortfolioId,
            command.Amount,
            command.AveragePrice,
            command.Invested);
        return portfolioAssetResult;
    }
}