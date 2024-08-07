namespace TokenVault.Domain.Entities;

public class PortfolioAsset
{
    public Guid CryptocurrencyId { get; set; }
    public Guid PortfolioId { get; set; }
    public double Holdings { get; set; }
    public double AveragePrice { get; set; }
    public double Invested { get; set; }

    public Portfolio? Portfolio { get; set; }
    public Cryptocurrency? Cryptocurrency { get; set; }
}