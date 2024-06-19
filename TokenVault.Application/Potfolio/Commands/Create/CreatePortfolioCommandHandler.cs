using MediatR;
using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Application.Potfolio.Common;
using TokenVault.Domain.Entities;

namespace TokenVault.Application.Potfolio.Commands.Create;

public class CreatePortfolioCommandHandler : IRequestHandler<CreatePortfolioCommand, PortfolioResult>
{
    private readonly IPortfolioRepository _portfolioRepository;

    public CreatePortfolioCommandHandler(IPortfolioRepository portfolioRepository)
    {
        _portfolioRepository = portfolioRepository;
    }

    public async Task<PortfolioResult> Handle(CreatePortfolioCommand command, CancellationToken cancellationToken)
    {
        var portfolio = new Portfolio
        {
            UserId = command.UserId,
            Title = command.Title
        };
        await _portfolioRepository.CreateAsync(portfolio);

        var portfolioResult = new PortfolioResult(
            portfolio.Id,
            portfolio.UserId,
            portfolio.Title);

        return portfolioResult;
    }
}