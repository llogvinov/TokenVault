using MapsterMapper;
using MediatR;
using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Application.Features.Portfolios.Common;

namespace TokenVault.Application.Features.Portfolios.Queries.GetPortfolioById;

public class GetPortfolioByIdQueryHandler : IRequestHandler<GetPortfolioByIdQuery, PortfolioResult>
{
     private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetPortfolioByIdQueryHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PortfolioResult> Handle(
        GetPortfolioByIdQuery query,
        CancellationToken cancellationToken)
    {
        var portfolio = await _unitOfWork.Portfolio.GetFirstOrDefaultAsync(p => p.Id == query.PortfolioId);
        if (portfolio is null)
        {
            throw new ArgumentNullException(nameof(portfolio),
                $"Portfolio with given id: {query.PortfolioId} does not exist");
        }

        var portfolioResult = _mapper.Map<PortfolioResult>(portfolio);
        return portfolioResult;
    }
}