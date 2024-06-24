using MapsterMapper;
using MediatR;
using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Application.Features.Cryptocurrencies.Common;
using TokenVault.Domain.Entities;

namespace TokenVault.Application.Features.Cryptocurrencies.Commands.CreateCryptocurrency;

public class CreateCryptocurrencyCommandHandler : 
    IRequestHandler<CreateCryptocurrencyCommand, CryptocurrencyResult>
{
    private readonly ICryptocurrencyRepository _cryptocurrencyRepository;
    private readonly IMapper _mapper;

    public CreateCryptocurrencyCommandHandler(
        ICryptocurrencyRepository cryptocurrencyRepository,
        IMapper mapper)
    {
        _cryptocurrencyRepository = cryptocurrencyRepository;
        _mapper = mapper;
    }

    public async Task<CryptocurrencyResult> Handle(
        CreateCryptocurrencyCommand command,
        CancellationToken cancellationToken)
    {
        var cryptocurrency = _mapper.Map<Cryptocurrency>(command);
        await _cryptocurrencyRepository.CreateAsync(cryptocurrency);

        var cryptocurrencyResult = _mapper.Map<CryptocurrencyResult>(cryptocurrency);
        return cryptocurrencyResult;
    }
}