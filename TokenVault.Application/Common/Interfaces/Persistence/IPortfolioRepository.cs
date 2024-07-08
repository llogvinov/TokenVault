using TokenVault.Domain.Entities;

namespace TokenVault.Application.Common.Interfaces.Persistence;

public interface IPortfolioRepository : IRepository<Portfolio>
{
    Task<Portfolio> UpdateAsync(Guid id, string title);
}