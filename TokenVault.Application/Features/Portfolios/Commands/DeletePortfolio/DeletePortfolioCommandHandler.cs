using MapsterMapper;
using MediatR;
using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Application.Features.Portfolios.Common;
using TokenVault.Domain.Entities;

namespace TokenVault.Application.Features.Portfolios.Commands.DeletePortfolio;

public class DeletePortfolioCommandHandler : 
    IRequestHandler<DeletePortfolioCommand, PortfolioResult>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DeletePortfolioCommandHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PortfolioResult> Handle(
        DeletePortfolioCommand command,
        CancellationToken cancellationToken)
    {
        var portfolio = await _unitOfWork.Portfolio.GetPortfolioByIdAsync(command.PortfolioId);
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

        _unitOfWork.Portfolio.Remove(portfolio);

        var result = _mapper.Map<PortfolioResult>(portfolioCopy);
        return result;
    }
}