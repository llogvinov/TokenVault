using MediatR;
using Microsoft.AspNetCore.Mvc;
using TokenVault.Application.Features.Cryptocurrencies.Commands.CreateCryptocurrency;
using TokenVault.Application.Features.Cryptocurrencies.Commands.DeleteCryptocurrency;
using TokenVault.Application.Features.Cryptocurrencies.Queries.GetCryptocurrencyById;
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
    /// get cryptocurrency by id  
    /// </summary>
    [HttpGet("{cryptocurrencyId}")]
    public async Task<IActionResult> GetCryptocurrency([FromRoute] Guid cryptocurrencyId)
    {
        var command = new GetCryptocurrencyByIdQuery(cryptocurrencyId);
        var result = await _mediatr.Send(command);

        var response = new CryptocurrencyResponse(
            result.Id,
            result.Symbol,
            result.Name);
        return Ok(response);
    }

    /// <summary>
    /// delete cryptocurrency 
    /// </summary>
    [HttpDelete("{cryptocurrencyId}")]
    public async Task<IActionResult> DeleteCryptocurrency([FromRoute] Guid cryptocurrencyId)
    {
        var command = new DeleteCryptocurrencyCommand(cryptocurrencyId);
        var response = await _mediatr.Send(command);

        return Ok(response);
    }
}