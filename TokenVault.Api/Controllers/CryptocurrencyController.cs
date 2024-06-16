using Microsoft.AspNetCore.Mvc;
using TokenVault.Contracts.Cryptocurrency;

namespace TokenVault.Api.Controllers;

public class CryptocurrencyController : ApiController
{
    private readonly CryptocurrencyService _cryptocurrencyService;

    public CryptocurrencyController(CryptocurrencyService cryptocurrencyService)
    {
        _cryptocurrencyService = cryptocurrencyService;
    }

    /// <summary>
    /// get all cryptocurrencies 
    /// </summary>
    [HttpGet]
    public IActionResult GetCryptocurrencies()
    {
        var cryptocurrencies = _cryptocurrencyService.GetCryptocurrencies();

        return Ok(cryptocurrencies);
    }

    /// <summary>
    /// add cryptocurrency 
    /// </summary>
    [HttpPost]
    public IActionResult AddCryptocurrency([FromBody] AddCryptocurrencyRequest request)
    {
        _cryptocurrencyService.AddCryptocurrency(request);

        return Ok(request);
    }

    /// <summary>
    /// delete cryptocurrency 
    /// </summary>
    [HttpDelete("delete/{cryptocurrencyId}")]
    public IActionResult DeleteCryptoCurrency([FromRoute] Guid cryptocurrencyId)
    {
        _cryptocurrencyService.DeleteCryptocurrency(cryptocurrencyId);
        
        return Ok();
    }
}