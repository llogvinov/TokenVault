using TokenVault.Domain.Entities;

namespace TokenVault.Application.Common.Interfaces.Persistence;

public interface ICryptocurrencyRepository
{
    Task CreateAsync(Cryptocurrency cryptocurrency);

    Task DeleteAsync(Guid cryptocurrencyId);
    
    Task<Cryptocurrency?> GetCryptocurrencyByIdAsync(Guid cryptocurrencyId);

    Task<IEnumerable<Cryptocurrency>> GetCryptocurrenciesAsync();
}