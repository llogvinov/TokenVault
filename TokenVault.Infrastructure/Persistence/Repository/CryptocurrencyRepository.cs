using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Application.Features.Cryptocurrencies.Common;
using TokenVault.Domain.Entities;

namespace TokenVault.Infrastructure.Persistence.Repository;

public class CryptocurrencyRepository : Repository<Cryptocurrency>, ICryptocurrencyRepository
{
    public CryptocurrencyRepository(TokenVaultDbContext dbContext)
        : base(dbContext) { }

    public async Task<Cryptocurrency> UpdateAsync(Guid id,
        UpdateCryptocurrencyDetails details)
    {
        var cryptocurrencyFromDb = await GetCryptocurrencyByIdAsync(id);
        if (cryptocurrencyFromDb is null)
        {
            throw new ArgumentNullException(nameof(cryptocurrencyFromDb),
                $"Cryptocurrency with given id: {id} does not exist");
        }

        cryptocurrencyFromDb.Symbol = details.Symbol ?? cryptocurrencyFromDb.Symbol;
        cryptocurrencyFromDb.Name = details.Name ?? cryptocurrencyFromDb.Name;
        return cryptocurrencyFromDb;
    }

    public async Task<List<Cryptocurrency>> GetCryptocurrenciesAsync()
    {
        var query = "SELECT * FROM Cryptocurrencies";
        return await QueryAsync(query);
    }

    public async Task<Cryptocurrency?> GetCryptocurrencyByIdAsync(Guid id)
    {
        var query = $"SELECT * FROM Cryptocurrencies WHERE Id = '{id}'";
        return await QueryFirstOrDefaultAsync(query);
    }
}