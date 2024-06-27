using TokenVault.Application.Features.Cryptocurrencies.Common;
using TokenVault.Domain.Entities;

namespace TokenVault.Application.Common.Interfaces.Persistence;

public interface ICryptocurrencyRepository : IRepository<Cryptocurrency>
{
    Task<Cryptocurrency> UpdateAsync(Guid id, UpdateCryptocurrencyDetails details);
}