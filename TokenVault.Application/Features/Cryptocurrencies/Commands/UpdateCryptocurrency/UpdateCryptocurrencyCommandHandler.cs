using MapsterMapper;
using MediatR;
using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Application.Features.Cryptocurrencies.Common;

namespace TokenVault.Application.Features.Cryptocurrencies.Commands.UpdateCryptocurrency;

public class UpdateCryptocurrencyCommandHandler :
    IRequestHandler<UpdateCryptocurrencyCommand, CryptocurrencyResult>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateCryptocurrencyCommandHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CryptocurrencyResult> Handle(
        UpdateCryptocurrencyCommand command,
        CancellationToken cancellationToken)
    {
        var details = _mapper.Map<UpdateCryptocurrencyDetails>(command);
        var cryptocurrency = await _unitOfWork.Cryptocurrency.UpdateAsync(command.CryptocurrencyId, details);

        var cryptocurrencyResult = _mapper.Map<CryptocurrencyResult>(cryptocurrency);
        return cryptocurrencyResult;
    }
}