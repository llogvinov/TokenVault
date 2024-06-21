using MapsterMapper;
using MediatR;
using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Application.Features.PortfolioAssets.Common;

namespace TokenVault.Application.Features.PortfolioAssets.Queries.GetPortfolioAssetsByPortfolioId;

public class GetPortfolioAssetsByPortfolioIdQueryHandler :
    IRequestHandler<GetPortfolioAssetsByPortfolioIdQuery, IEnumerable<PortfolioAssetResult>>
{
    private readonly IPortfolioAssetRepository _portfolioAssetRepository;
    private readonly IMapper _mapper;

    public GetPortfolioAssetsByPortfolioIdQueryHandler(
        IPortfolioAssetRepository portfolioAssetRepository,
        IMapper mapper)
    {
        _portfolioAssetRepository = portfolioAssetRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PortfolioAssetResult>> Handle(
        GetPortfolioAssetsByPortfolioIdQuery query,
        CancellationToken cancellationToken)
    {
        var portfolioAssets = await _portfolioAssetRepository.GetPortfolioAssetsAsync(query.PortfolioId);

        List<PortfolioAssetResult> portfolioAssetsResult = new ();
        foreach (var portfolioAsset in portfolioAssets)
        {
            var portfolioAssetResult = _mapper.Map<PortfolioAssetResult>(portfolioAsset);
            portfolioAssetsResult.Add(portfolioAssetResult);
        }

        return portfolioAssetsResult;
    }
}