using MapsterMapper;
using MediatR;
using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Application.Features.PortfolioAssets.Common;

namespace TokenVault.Application.Features.PortfolioAssets.Queries.GetPortfolioAssetsByPortfolioId;

public class GetPortfolioAssetsByPortfolioIdQueryHandler :
    IRequestHandler<GetPortfolioAssetsByPortfolioIdQuery, IEnumerable<PortfolioAssetResult>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetPortfolioAssetsByPortfolioIdQueryHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PortfolioAssetResult>> Handle(
        GetPortfolioAssetsByPortfolioIdQuery query,
        CancellationToken cancellationToken)
    {
        var portfolioAssets = await _unitOfWork.PortfolioAsset.GetAllAsync(
            a => a.PortfolioId == query.PortfolioId);

        List<PortfolioAssetResult> portfolioAssetsResult = new ();
        foreach (var portfolioAsset in portfolioAssets)
        {
            var portfolioAssetResult = _mapper.Map<PortfolioAssetResult>(portfolioAsset);
            portfolioAssetsResult.Add(portfolioAssetResult);
        }

        return portfolioAssetsResult;
    }
}