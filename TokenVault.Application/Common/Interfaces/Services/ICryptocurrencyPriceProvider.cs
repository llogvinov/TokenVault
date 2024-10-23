namespace TokenVault.Application.Common.Interfaces.Services;

public interface ICryptocurrencyPriceProvider
{
    Task<double> GetPrice(string symbol);
}