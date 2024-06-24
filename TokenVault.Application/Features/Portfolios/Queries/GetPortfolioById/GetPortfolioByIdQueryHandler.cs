using MapsterMapper;
using MediatR;
using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Application.Features.Portfolios.Common;

namespace TokenVault.Application.Features.Portfolios.Queries.GetPortfolioById;

public class GetPortfolioByIdQueryHandler : IRequestHandler<GetPortfolioByIdQuery, PortfolioResult>
{
    private readonly IPortfolioRepository _portfolioRepository;
    private readonly IMapper _mapper;

    public GetPortfolioByIdQueryHandler(
        IPortfolioRepository portfolioRepository,
        IMapper mapper)
    {
        _portfolioRepository = portfolioRepository;
        _mapper = mapper;
    }

    public async Task<PortfolioResult> Handle(
        GetPortfolioByIdQuery query,
        CancellationToken cancellationToken)
    {
        var portfolio = await _portfolioRepository.GetPortfolioByIdAsync(query.PortfolioId);
        if (portfolio is null)
        {
            throw new ArgumentNullException(nameof(portfolio),
                $"Portfolio with given id: {query.PortfolioId} does not exist");
        }

        var portfolioResult = _mapper.Map<PortfolioResult>(portfolio);
        return portfolioResult;
    }
}