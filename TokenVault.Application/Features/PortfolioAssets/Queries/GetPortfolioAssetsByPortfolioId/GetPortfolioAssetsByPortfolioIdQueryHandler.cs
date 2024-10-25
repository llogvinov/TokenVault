using MapsterMapper;
using MediatR;
using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Application.Common.Interfaces.Services;
using TokenVault.Application.Features.PortfolioAssets.Common;

namespace TokenVault.Application.Features.PortfolioAssets.Queries.GetPortfolioAssetsByPortfolioId;

public class GetPortfolioAssetsByPortfolioIdQueryHandler :
    IRequestHandler<GetPortfolioAssetsByPortfolioIdQuery, IEnumerable<PortfolioAssetResult>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ICryptocurrencyPriceProvider _cryptocurrencyPriceProvider;

    public GetPortfolioAssetsByPortfolioIdQueryHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ICryptocurrencyPriceProvider cryptocurrencyPriceProvider)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _cryptocurrencyPriceProvider = cryptocurrencyPriceProvider;
    }

    public async Task<IEnumerable<PortfolioAssetResult>> Handle(
        GetPortfolioAssetsByPortfolioIdQuery query,
        CancellationToken cancellationToken)
    {
        string sql = "SELECT * FROM PortfolioAssets " +
            $"WHERE PortfolioId = {query.PortfolioId}";
        var portfolioAssets = await _unitOfWork.PortfolioAsset.QueryAsync(sql);

        List<PortfolioAssetResult> portfolioAssetsResult = new ();
        foreach (var portfolioAsset in portfolioAssets)
        {
            var cryptocurrency = await _unitOfWork.Cryptocurrency.GetCryptocurrencyByIdAsync(portfolioAsset.CryptocurrencyId);
            if (cryptocurrency is null)
            {
                throw new Exception("cryptocurrency not found");
            }

            var price = await _cryptocurrencyPriceProvider.GetPrice(cryptocurrency.Symbol);
            var profit = portfolioAsset.Holdings * price - portfolioAsset.Invested;
            var portfolioAssetResult = _mapper.Map<PortfolioAssetResult>((portfolioAsset, price, profit));
            portfolioAssetsResult.Add(portfolioAssetResult);
        }

        return portfolioAssetsResult;
    }
}