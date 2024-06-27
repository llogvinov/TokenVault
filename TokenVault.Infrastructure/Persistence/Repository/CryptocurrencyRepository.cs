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
        var cryptocurrencyFromDb = await GetFirstOrDefaultAsync(c => c.Id == id);
        if (cryptocurrencyFromDb is null)
        {
            throw new ArgumentNullException(nameof(cryptocurrencyFromDb),
                $"Cryptocurrency with given id: {id} does not exist");
        }

        cryptocurrencyFromDb.Symbol = details.Symbol ?? cryptocurrencyFromDb.Symbol;
        cryptocurrencyFromDb.Name = details.Name ?? cryptocurrencyFromDb.Name;

        return cryptocurrencyFromDb;
    }
}