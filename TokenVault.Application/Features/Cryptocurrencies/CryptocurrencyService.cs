
using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Application.Features.Cryptocurrencies.Commands.CreateCryptocurrency;
using TokenVault.Contracts.Cryptocurrency;
using TokenVault.Domain.Entities;

public class CryptocurrencyService
{
    private readonly ICryptocurrencyRepository _cryptocurrencyRepository;

    public CryptocurrencyService(ICryptocurrencyRepository cryptocurrencyRepository)
    {
        _cryptocurrencyRepository = cryptocurrencyRepository;
    }

    public void DeleteCryptocurrency(Guid cryptocurrencyId)
    {
        _cryptocurrencyRepository.Delete(cryptocurrencyId);
    }

    public IEnumerable<Cryptocurrency> GetCryptocurrencies()
    {
        return _cryptocurrencyRepository.GetAll();
    }
}