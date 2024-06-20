using MapsterMapper;
using MediatR;
using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Application.Features.Potfolios.Common;
using TokenVault.Application.Transactions;
using TokenVault.Domain.Entities;

namespace TokenVault.Application.Features.Potfolios.Commands.DeletePortfolio;

public class DeletePortfolioCommandHandler : IRequestHandler<DeletePortfolioCommand, PortfolioResult>
{
    private readonly IPortfolioRepository _portfolioRepository;
    private readonly IMapper _mapper;

    public DeletePortfolioCommandHandler(
        IPortfolioRepository portfolioRepository,
        IMapper mapper)
    {
        _portfolioRepository = portfolioRepository;
        _mapper = mapper;
    }

    public async Task<PortfolioResult> Handle(DeletePortfolioCommand command, CancellationToken cancellationToken)
    {
        var portfolio = await _portfolioRepository.GetPortfolioByIdAsync(command.PortfolioId);
        if (portfolio is null)
        {
            throw new ArgumentNullException(nameof(portfolio),
                $"Portfolio with given id: {command.PortfolioId} does not exist");
        }
        var portfolioCopy = new Portfolio
        {
            Id = command.PortfolioId,
            UserId = portfolio.UserId,
            Title = portfolio.Title,
        };

        await _portfolioRepository.DeleteAsync(command.PortfolioId);

        var portfolioResult = _mapper.Map<PortfolioResult>(portfolioCopy);
        return portfolioResult;
    }
}