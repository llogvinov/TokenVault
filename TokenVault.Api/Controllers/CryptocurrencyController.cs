using MediatR;
using Microsoft.AspNetCore.Mvc;
using TokenVault.Application.Features.Cryptocurrencies.Commands.CreateCryptocurrency;
using TokenVault.Contracts.Cryptocurrency;

namespace TokenVault.Api.Controllers;

public class CryptocurrencyController : ApiController
{
    private readonly CryptocurrencyService _cryptocurrencyService;
    private readonly ISender _mediatr;

    public CryptocurrencyController(CryptocurrencyService cryptocurrencyService, ISender mediatr)
    {
        _cryptocurrencyService = cryptocurrencyService;
        _mediatr = mediatr;
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
    public async Task<IActionResult> AddCryptocurrency([FromBody] AddCryptocurrencyRequest request)
    {
        var command = new CreateCryptocurrencyCommand(request.Symbol, request.Name); // todo: use mapper
        var response = await _mediatr.Send(command);

        return Ok(response);
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