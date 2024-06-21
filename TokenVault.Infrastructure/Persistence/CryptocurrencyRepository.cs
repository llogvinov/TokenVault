using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Application.Features.Cryptocurrencies.Common;
using TokenVault.Domain.Entities;

namespace TokenVault.Infrastructure.Persistence;

public class CryptocurrencyRepository : ICryptocurrencyRepository
{
    private static readonly List<Cryptocurrency> _cryptocurrencies = new();

    public async Task CreateAsync(Cryptocurrency cryptocurrency)
    {
        await Task.CompletedTask;
        
        _cryptocurrencies.Add(cryptocurrency);
    }

    public async Task DeleteAsync(Guid cryptocurrencyId)
    {
        await Task.CompletedTask;

        var cryptocurrency = _cryptocurrencies.FirstOrDefault(c => c.Id == cryptocurrencyId);
        if (cryptocurrency is not null)
        {
            _cryptocurrencies.Remove(cryptocurrency);
        }
    }

    public async Task<Cryptocurrency?> GetCryptocurrencyByIdAsync(Guid cryptocurrencyId)
    {
        await Task.CompletedTask;

        var cryptocurrency = _cryptocurrencies.FirstOrDefault(c => c.Id == cryptocurrencyId);
        if (cryptocurrency is not null)
        {
            return cryptocurrency;
        }

        return default;
    }

    public async Task<Cryptocurrency> UpdateAsync(
        Guid cryptocurrencyId,
        UpdateCryptocurrencyDetails details)
    {
        await Task.CompletedTask;

        var cryptocurrency = _cryptocurrencies.FirstOrDefault(c => c.Id == cryptocurrencyId);
        if (cryptocurrency is null)
        {
            throw new ArgumentNullException(nameof(cryptocurrency),
                $"Cryptocurrency with given id: {cryptocurrencyId} does not exist");
        }

        cryptocurrency.Symbol = details.Symbol ?? cryptocurrency.Symbol;
        cryptocurrency.Name = details.Name ?? cryptocurrency.Name;

        return cryptocurrency;
    }

    public async Task<IEnumerable<Cryptocurrency>> GetCryptocurrenciesAsync()
    {
        await Task.CompletedTask;

        return _cryptocurrencies;
    }
}