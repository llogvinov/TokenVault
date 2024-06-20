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

    private UpdatePortfolioAssetDetails GetUpdatedAssetDetails(
        PortfolioAsset asset,
        UpdatePortfolioAssetDetails updatePortfolioAssetDetails)
    {
        var amount = asset.Amount + updatePortfolioAssetDetails.Amount;
        var invested = asset.Invested + updatePortfolioAssetDetails.Invested;
        var averagePrice = invested / amount;

        var updateAssetDetails = new UpdatePortfolioAssetDetails(amount, averagePrice, invested);
        return updateAssetDetails;
    }
}