namespace TokenVault.Domain.Entities;

public class Cryptocurrency
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Symbol { get; set; } = null!;
    public string Name { get; set; } = null!;

    public ICollection<PortfolioAsset>? PortfolioAssets { get; set; }
}