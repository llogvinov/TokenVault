namespace TokenVault.Domain.Entities;

public class Portfolio
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public string Title { get; set; } = null!;

    public ICollection<PortfolioAsset>? PortfolioAssets { get; set; }
}