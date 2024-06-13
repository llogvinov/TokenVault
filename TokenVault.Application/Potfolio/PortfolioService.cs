using MapsterMapper;
using MediatR;
using TokenVault.Application.Potfolio.Commands.Create;
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
        var portfolio = await _mediator.Send(command);

        var response = _mapper.Map<CreatePortfolioResponse>(portfolio);

        return response;
    }
}