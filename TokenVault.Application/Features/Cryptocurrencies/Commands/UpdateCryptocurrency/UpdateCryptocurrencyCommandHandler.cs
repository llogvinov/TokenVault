using MapsterMapper;
using MediatR;
using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Application.Features.Cryptocurrencies.Common;

namespace TokenVault.Application.Features.Cryptocurrencies.Commands.UpdateCryptocurrency;

public class UpdateCryptocurrencyCommandHandler :
    IRequestHandler<UpdateCryptocurrencyCommand, CryptocurrencyResult>
{
    private readonly ICryptocurrencyRepository _cryptocurrencyRepository;
    private readonly IMapper _mapper;

    public UpdateCryptocurrencyCommandHandler(
        ICryptocurrencyRepository cryptocurrencyRepository,
        IMapper mapper)
    {
        _cryptocurrencyRepository = cryptocurrencyRepository;
        _mapper = mapper;
    }

    public async Task<CryptocurrencyResult> Handle(
        UpdateCryptocurrencyCommand command,
        CancellationToken cancellationToken)
    {
        var details = _mapper.Map<UpdateCryptocurrencyDetails>(command);
        var cryptocurrency = await _cryptocurrencyRepository.UpdateAsync(command.CryptocurrencyId, details);

        var cryptocurrencyResult = _mapper.Map<CryptocurrencyResult>(cryptocurrency);
        return cryptocurrencyResult;
    }
}