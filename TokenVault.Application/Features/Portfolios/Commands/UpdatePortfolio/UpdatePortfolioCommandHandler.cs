using MapsterMapper;
using MediatR;
using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Application.Features.Portfolios.Common;

namespace TokenVault.Application.Features.Portfolios.Commands.UpdatePortfolio;

public class UpdatePortfolioCommandHandler :
    IRequestHandler<UpdatePortfolioCommand, PortfolioResult>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdatePortfolioCommandHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PortfolioResult> Handle(
        UpdatePortfolioCommand command,
        CancellationToken cancellationToken)
    {
        var portfolio = await _unitOfWork.Portfolio.UpdateAsync(command.Id, command.Title);

        var result = _mapper.Map<PortfolioResult>(portfolio);
        return result;
    }
}