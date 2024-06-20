using MediatR;
using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Domain.Entities;

namespace TokenVault.Application.Features.PortfolioAssets.Queries.GetPortfolioAssetsByPortfolioId;

public class GetPortfolioAssetsByPortfolioIdQueryHandler :
    IRequestHandler<GetPortfolioAssetsByPortfolioIdQuery, IEnumerable<PortfolioAsset>>
{
    private readonly IPortfolioAssetRepository _portfolioAssetRepository;

    public GetPortfolioAssetsByPortfolioIdQueryHandler(IPortfolioAssetRepository portfolioAssetRepository)
    {
        _portfolioAssetRepository = portfolioAssetRepository;
    }

    public async Task<IEnumerable<PortfolioAsset>> Handle(
        GetPortfolioAssetsByPortfolioIdQuery query,
        CancellationToken cancellationToken)
    {
        var portfolioAssets = await _portfolioAssetRepository.GetPortfolioAssetsAsync(query.PortfolioId);
        return portfolioAssets;
    }
}