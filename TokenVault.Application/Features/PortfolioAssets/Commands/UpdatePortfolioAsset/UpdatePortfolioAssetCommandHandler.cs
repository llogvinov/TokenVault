using MapsterMapper;
using MediatR;
using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Application.Features.PortfolioAssets.Common;
using TokenVault.Domain.Entities;

namespace TokenVault.Application.Features.PortfolioAssets.Commands.UpdatePortfolioAsset;

public class UpdatePortfolioAssetCommandHandler :
    IRequestHandler<UpdatePortfolioAssetCommand, PortfolioAssetResult>
{
    private readonly IPortfolioAssetRepository _portfolioAssetRepository;
    private readonly IMapper _mapper;

    public UpdatePortfolioAssetCommandHandler(
        IPortfolioAssetRepository portfolioAssetRepository,
        IMapper mapper)
    {
        _portfolioAssetRepository = portfolioAssetRepository;
        _mapper = mapper;
    }

    public async Task<PortfolioAssetResult> Handle(
        UpdatePortfolioAssetCommand command,
        CancellationToken cancellationToken)
    {
        PortfolioAssetResult portfolioAssetResult;

        var portfolioAsset = await _portfolioAssetRepository.GetPortfolioAssetAsync(
            command.CryptocurrencyId, command.PortfolioId);

        if (portfolioAsset is null)
        {
            if (command.UpdatePortfolioAssetDetails.TransactionType == (int)TransactionType.Sell)
            {
                throw new Exception("You do not have enough assets to sell");
            }

            portfolioAsset = _mapper.Map<PortfolioAsset>(command);
            await _portfolioAssetRepository.CreateAsync(portfolioAsset);

            portfolioAssetResult = _mapper.Map<PortfolioAssetResult>(portfolioAsset);
        }
        else
        {
            var updatePortfolioAssetDetails = GetUpdatedAssetDetails(
                portfolioAsset, command.UpdatePortfolioAssetDetails);

            var updatedPortfolioAsset = await _portfolioAssetRepository.UpdateAsync(
                command.CryptocurrencyId,
                command.PortfolioId,
                updatePortfolioAssetDetails);

            portfolioAssetResult = _mapper.Map<PortfolioAssetResult>(updatedPortfolioAsset);
        }

        return portfolioAssetResult;
    }

    private UpdatedAssetDetails GetUpdatedAssetDetails(
        PortfolioAsset asset,
        UpdatePortfolioAssetDetails updatePortfolioAssetDetails)
    {
        double holdings = 0;
        double invested = 0;
        double averagePrice = 0;

        switch (updatePortfolioAssetDetails.TransactionType)
        {
            case (int)TransactionType.Buy:
                holdings = asset.Holdings + updatePortfolioAssetDetails.Amount;
                invested = asset.Invested + updatePortfolioAssetDetails.TotalPrice;
                averagePrice = invested / holdings;
                break;
            case (int)TransactionType.Sell:
                holdings = asset.Holdings - updatePortfolioAssetDetails.Amount;
                if (holdings < 0)
                {
                    throw new Exception("You do not have enough assets to sell");
                }
                else if (holdings == 0)
                {
                    invested = 0;
                    averagePrice = 0;
                    break;
                }

                invested = asset.Invested - updatePortfolioAssetDetails.TotalPrice;
                averagePrice = invested / holdings;
                break;
        }

        var updateAssetDetails = new UpdatedAssetDetails(holdings, averagePrice, invested);
        return updateAssetDetails;
    }
}