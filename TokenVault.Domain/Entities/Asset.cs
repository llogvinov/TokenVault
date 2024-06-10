namespace TokenVault.Domain.Entities;

public class Asset
{
    public string Symbol { get; set; } = null!;
    public Guid PortfolioId { get; set; }
    public string Name { get; set; } = null!;
    public double Holdings { get; set; }
    public double Invested { get; set; }
    public double AveragePrice { get; set; }
}