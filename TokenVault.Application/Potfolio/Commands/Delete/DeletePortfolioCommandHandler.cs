using MediatR;
using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Application.Potfolio.Common;

namespace TokenVault.Application.Potfolio.Commands.Delete;

public class DeletePortfolioCommandHandler : IRequestHandler<DeletePortfolioCommand, PortfolioResult>
{
    private readonly IPortfolioRepository _portfolioRepository;

    public DeletePortfolioCommandHandler(IPortfolioRepository portfolioRepository)
    {
        _portfolioRepository = portfolioRepository;
    }

    public async Task<PortfolioResult> Handle(DeletePortfolioCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        _portfolioRepository.Delete(command.PortfolioId);
        var portfolioResult = new PortfolioResult(command.PortfolioId);

        return portfolioResult;
    }
}