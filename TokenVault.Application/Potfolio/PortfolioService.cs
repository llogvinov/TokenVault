using MapsterMapper;
using MediatR;
using TokenVault.Application.Potfolio.Commands.Create;
using TokenVault.Application.Potfolio.Commands.Delete;
using TokenVault.Application.Potfolio.Common;
using TokenVault.Contracts.Portfolio;

namespace TokenVault.Application.Potfolio;

public class PortfolioService
{
    private ISender _mediator;
    private IMapper _mapper;

    public PortfolioService(ISender mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task<CreatePortfolioResponse> CreatePortfolio(CreatePortfolioRequest request, Guid userId)
    {
        var command = new CreatePortfolioCommand(request.Title, userId);
        var createPortfolioResult = await _mediator.Send(command);

        var response = new CreatePortfolioResponse(
            createPortfolioResult.PortfolioId,
            createPortfolioResult.UserId,
            createPortfolioResult.Title);

        return response;
    }

    public async Task<PortfolioResult> DeletePortfolio(Guid portfolioId)
    {
        var command = new DeletePortfolioCommand(portfolioId);
        var portfolioResult = await _mediator.Send(command);

        return portfolioResult;
    }
}