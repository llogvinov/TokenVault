using MediatR;
using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Contracts.Cryptocurrency;
using TokenVault.Domain.Entities;

namespace TokenVault.Application.Features.Cryptocurrencies.Commands.CreateCryptocurrency;

public class CreateCryptocurrencyCommandHandler : IRequestHandler<CreateCryptocurrencyCommand, CreateCryptocurrencyResponse>
{
    private readonly ICryptocurrencyRepository _cryptocurrencyRepository;

    public CreateCryptocurrencyCommandHandler(ICryptocurrencyRepository cryptocurrencyRepository)
    {
        _cryptocurrencyRepository = cryptocurrencyRepository;
    }

    public async Task<CreateCryptocurrencyResponse> Handle(
        CreateCryptocurrencyCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        var cryptocurrency = new Cryptocurrency
        {
            Symbol = command.Symbol,
            Name = command.Name,
        };
        _cryptocurrencyRepository.Add(cryptocurrency);

        var createResponse = new CreateCryptocurrencyResponse(
            cryptocurrency.Id,
            cryptocurrency.Symbol,
            cryptocurrency.Name);
        return createResponse;
    }
}