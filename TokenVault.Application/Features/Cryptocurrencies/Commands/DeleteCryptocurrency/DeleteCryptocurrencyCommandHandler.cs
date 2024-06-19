using MapsterMapper;
using MediatR;
using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Application.Features.Cryptocurrencies.Common;

namespace TokenVault.Application.Features.Cryptocurrencies.Commands.DeleteCryptocurrency;

public class DeleteCryptocurrencyCommandHandler : IRequestHandler<DeleteCryptocurrencyCommand, CryptocurrencyResult>
{
    private readonly ICryptocurrencyRepository _cryptocurrencyRepository;
    private readonly IMapper _mapper;

    public DeleteCryptocurrencyCommandHandler(
        ICryptocurrencyRepository cryptocurrencyRepository,
        IMapper mapper)
    {
        _cryptocurrencyRepository = cryptocurrencyRepository;
        _mapper = mapper;
    }

    public async Task<CryptocurrencyResult> Handle(
        DeleteCryptocurrencyCommand command,
        CancellationToken cancellationToken)
    {
        var cryptocurrency = _cryptocurrencyRepository.GetCryptocurrencyByIdAsync(command.CryptocurrencyId);
        if (cryptocurrency is null)
        {
            throw new ArgumentNullException(nameof(cryptocurrency), 
                $"Cryptocurrency with given id: {command.CryptocurrencyId} does not exist");
        }
        await _cryptocurrencyRepository.DeleteAsync(command.CryptocurrencyId);

        var cryptocurrencyResult = _mapper.Map<CryptocurrencyResult>(cryptocurrency);
        return cryptocurrencyResult;
    }
}