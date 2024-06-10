using MediatR;
using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Domain.Entities;

public class CreatePortfolioCommandHandler : IRequestHandler<CreatePortfolioCommand, Portfolio>
{
    private readonly IPortfolioRepository _portfolioRepository;

    public CreatePortfolioCommandHandler(IPortfolioRepository portfolioRepository)
    {
        _portfolioRepository = portfolioRepository;
    }

    public async Task<Portfolio> Handle(CreatePortfolioCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        var portfolio = new Portfolio
        {
            UserId = command.UserId,
            Title = command.Title
        };
        _portfolioRepository.Add(portfolio);

        return portfolio;
    }
}