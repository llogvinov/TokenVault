
using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Domain.Entities;

public class CryptocurrencyService
{
    private readonly ICryptocurrencyRepository _cryptocurrencyRepository;

    public CryptocurrencyService(ICryptocurrencyRepository cryptocurrencyRepository)
    {
        _cryptocurrencyRepository = cryptocurrencyRepository;
    }

    public IEnumerable<Cryptocurrency> GetCryptocurrencies()
    {
        return _cryptocurrencyRepository.GetAll();
    }
}