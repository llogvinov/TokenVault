using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Domain.Entities;

public class PortfolioRepository : IPortfolioRepository
{
    private static readonly List<Portfolio> _portfolios = new();

    public void Add(Portfolio portfolio)
    {
        _portfolios.Add(portfolio);
    }

    public Portfolio? GetPortfolioById(Guid id)
    {
        return _portfolios.SingleOrDefault(p => p.Id == id);
    }
}