using MediatR;
using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Application.Potfolio.Common;
using TokenVault.Application.Transactions;

namespace TokenVault.Application.Potfolio.Commands.Delete;

public class DeletePortfolioCommandHandler : IRequestHandler<DeletePortfolioCommand, PortfolioResult>
{
    private readonly IPortfolioRepository _portfolioRepository;
    private readonly TransactionsService _transactionsService;

    public DeletePortfolioCommandHandler(IPortfolioRepository portfolioRepository, TransactionsService transactionsService)
    {
        _portfolioRepository = portfolioRepository;
        _transactionsService = transactionsService;
    }

    public async Task<PortfolioResult> Handle(DeletePortfolioCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        _portfolioRepository.Delete(command.PortfolioId);
        _transactionsService.DeleteAllPortfolioTransactions(command.PortfolioId);
        
        var portfolioResult = new PortfolioResult(command.PortfolioId);

        return portfolioResult;
    }
}