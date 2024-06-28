using MapsterMapper;
using MediatR;
using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Application.Features.Portfolios.Common;
using TokenVault.Domain.Entities;

namespace TokenVault.Application.Features.Portfolios.Commands.CreatePortfolio;

public class CreatePortfolioCommandHandler : IRequestHandler<CreatePortfolioCommand, PortfolioResult>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreatePortfolioCommandHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PortfolioResult> Handle(
        CreatePortfolioCommand command,
        CancellationToken cancellationToken)
    {
        var portfolio = _mapper.Map<Portfolio>(command);
        await _unitOfWork.Portfolio.AddAsync(portfolio);

        var portfolioResult = _mapper.Map<PortfolioResult>(portfolio);
        return portfolioResult;
    }
}