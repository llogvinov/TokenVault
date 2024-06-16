namespace TokenVault.Domain.Entities;

public class Asset
{
    public Guid CryptocurrencyId { get; set; }
    public Guid PortfolioId { get; set; }
    public double Amount { get; set; }
    public double AveragePrice { get; set; }
    public double Invested { get; set; }
}