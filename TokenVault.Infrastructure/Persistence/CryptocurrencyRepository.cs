using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Domain.Entities;

namespace TokenVault.Infrastructure.Persistence;

public class CryptocurrencyRepository : ICryptocurrencyRepository
{
    private static readonly List<Cryptocurrency> _cryptocurrencies = new();

    public void Add(Cryptocurrency cryptocurrency)
    {
        _cryptocurrencies.Add(cryptocurrency);
    }

    public void Delete(Guid cryptocurrencyId)
    {
        var cryptocurrency = _cryptocurrencies.FirstOrDefault(c => c.Id == cryptocurrencyId);
        if (cryptocurrency is not null)
        {
            _cryptocurrencies.Remove(cryptocurrency);
        }
    }

    public IEnumerable<Cryptocurrency> GetAll()
    {
        return _cryptocurrencies;
    }

    public Cryptocurrency? GetCryptocurrencyById(Guid cryptocurrencyId)
    {
        var cryptocurrency = _cryptocurrencies.FirstOrDefault(c => c.Id == cryptocurrencyId);
        if (cryptocurrency is not null)
        {
            return cryptocurrency;
        }

        return default;
    }
}