using System.Globalization;
using TokenVault.Application.Common.Interfaces.Services;

namespace TokenVault.Infrastructure.Services;

public class SimpleAPICryptocurrencyPriceProvider : ICryptocurrencyPriceProvider
{
    private readonly HttpClient _httpClient;

    public SimpleAPICryptocurrencyPriceProvider(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<double> GetPrice(string symbol)
    {
        var response = await _httpClient.GetStringAsync($"https://cryptoprices.cc/{symbol}");
        Console.WriteLine($"Response from API: {response}");
        response = response.Trim();

        if (decimal.TryParse(response, CultureInfo.InvariantCulture, out var price))
        {
            var dPrice = decimal.ToDouble(price);
            return dPrice;
        }
        else
        {
            throw new Exception($"Invalid response format from {symbol} price endpoint.");
        }
    }
}