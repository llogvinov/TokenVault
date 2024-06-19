using MediatR;
using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Application.Potfolio.Common;
using TokenVault.Application.Transactions;
using TokenVault.Domain.Entities;

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
        var portfolio = await _portfolioRepository.GetPortfolioByIdAsync(command.PortfolioId);
        if (portfolio is null)
        {
            throw new ArgumentNullException(nameof(portfolio), 
                $"portfolio with given id: {command.PortfolioId} does not exist");
        }
        var portfolioCopy = new Portfolio
        {
            Id = command.PortfolioId,
            UserId = portfolio.UserId,
            Title = portfolio.Title,
        };

        await _portfolioRepository.DeleteAsync(command.PortfolioId);
        _transactionsService.DeleteAllPortfolioTransactions(command.PortfolioId);
        
        var portfolioResult = new PortfolioResult(
            portfolioCopy.Id,
            portfolioCopy.UserId,
            portfolioCopy.Title);

        return portfolioResult;
    }
}