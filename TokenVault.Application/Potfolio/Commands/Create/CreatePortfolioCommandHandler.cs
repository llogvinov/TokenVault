using MediatR;
using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Application.Potfolio.Common;
using TokenVault.Domain.Entities;

namespace TokenVault.Application.Potfolio.Commands.Create;

public class CreatePortfolioCommandHandler : IRequestHandler<CreatePortfolioCommand, CreatePortfolioResult>
{
    private readonly IPortfolioRepository _portfolioRepository;

    public CreatePortfolioCommandHandler(IPortfolioRepository portfolioRepository)
    {
        _portfolioRepository = portfolioRepository;
    }

    public async Task<CreatePortfolioResult> Handle(CreatePortfolioCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        var portfolio = new Portfolio
        {
            UserId = command.UserId,
            Title = command.Title
        };
        _portfolioRepository.Add(portfolio);

        var portfolioResult = new CreatePortfolioResult(
            portfolio.Id,
            portfolio.UserId,
            portfolio.Title);

        return portfolioResult;
    }
}