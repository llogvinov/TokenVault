namespace TokenVault.Domain.Entities;

public class Transaction
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId {get; set;}
    public Guid PortfolioId { get; set; }
    public Guid CryptocurrencyId { get; set; }
    public string AssetSymbol { get; set; } = null!;
    public double Quantity { get; set; }
    public double Price { get; set; }
    public double Total { get; set; }
    public DateTime CreateDate { get; set; } = DateTime.Now;
}