using MediatR;
using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Contracts.Cryptocurrency;

namespace TokenVault.Application.Features.Cryptocurrencies.Commands.DeleteCryptocurrency;

public class DeleteCryptocurrencyCommandHandler : IRequestHandler<DeleteCryptocurrencyCommand, CryptocurrencyResponse>
{
    private readonly ICryptocurrencyRepository _cryptocurrencyRepository;

    public DeleteCryptocurrencyCommandHandler(ICryptocurrencyRepository cryptocurrencyRepository)
    {
        _cryptocurrencyRepository = cryptocurrencyRepository;
    }

    public async Task<CryptocurrencyResponse> Handle(
        DeleteCryptocurrencyCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        var cryptocurrency = _cryptocurrencyRepository.GetCryptocurrency(command.CryptocurrencyId);
        if (cryptocurrency is null)
        {
            throw new ArgumentNullException(nameof(cryptocurrency), 
                $"Cryptocurrency with given id: {command.CryptocurrencyId} does not exist");
        }
        _cryptocurrencyRepository.Delete(command.CryptocurrencyId);

        var cryptocurrencyResponse = new CryptocurrencyResponse(
            cryptocurrency.Id,
            cryptocurrency.Symbol,
            cryptocurrency.Name);
        return cryptocurrencyResponse;
    }
}