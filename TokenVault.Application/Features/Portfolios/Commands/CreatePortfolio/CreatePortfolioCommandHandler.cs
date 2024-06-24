using MapsterMapper;
using MediatR;
using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Application.Features.Portfolios.Common;
using TokenVault.Domain.Entities;

namespace TokenVault.Application.Features.Portfolios.Commands.CreatePortfolio;

public class CreatePortfolioCommandHandler : IRequestHandler<CreatePortfolioCommand, PortfolioResult>
{
    private readonly IPortfolioRepository _portfolioRepository;
    private readonly IMapper _mapper;

    public CreatePortfolioCommandHandler(
        IPortfolioRepository portfolioRepository,
        IMapper mapper)
    {
        _portfolioRepository = portfolioRepository;
        _mapper = mapper;
    }

    public async Task<PortfolioResult> Handle(
        CreatePortfolioCommand command,
        CancellationToken cancellationToken)
    {
        var portfolio = _mapper.Map<Portfolio>(command);
        await _portfolioRepository.CreateAsync(portfolio);

        var portfolioResult = _mapper.Map<PortfolioResult>(portfolio);
        return portfolioResult;
    }
}