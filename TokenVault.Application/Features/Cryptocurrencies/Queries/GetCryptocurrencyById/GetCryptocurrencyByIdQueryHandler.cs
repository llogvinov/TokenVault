using MediatR;
using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Application.Features.Cryptocurrencies.Common;

namespace TokenVault.Application.Features.Cryptocurrencies.Queries.GetCryptocurrencyById;

public class GetCryptocurrencyByIdQueryHandler : IRequestHandler<GetCryptocurrencyByIdQuery, CryptocurrencyResult>
{
    private readonly ICryptocurrencyRepository _cryptocurrencyRepository;

    public GetCryptocurrencyByIdQueryHandler(ICryptocurrencyRepository cryptocurrencyRepository)
    {
        _cryptocurrencyRepository = cryptocurrencyRepository;
    }

    public async Task<CryptocurrencyResult> Handle(
        GetCryptocurrencyByIdQuery query,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        var cryptocurrency = _cryptocurrencyRepository.GetCryptocurrencyById(query.CryptocurrencyId);
        if (cryptocurrency == null)
        {
            throw new ArgumentNullException(nameof(cryptocurrency), 
                $"Cryptocurrency with given id: {query.CryptocurrencyId} does not exist");
        }

        var cryptocurrencyResult = new CryptocurrencyResult(
            cryptocurrency.Id,
            cryptocurrency.Symbol,
            cryptocurrency.Name);
        return cryptocurrencyResult;
    }
}
