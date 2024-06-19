using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Application.Features.Cryptocurrencies.Commands.CreateCryptocurrency;
using TokenVault.Application.Features.Cryptocurrencies.Commands.DeleteCryptocurrency;
using TokenVault.Application.Features.Cryptocurrencies.Queries.GetCryptocurrencyById;
using TokenVault.Contracts.Cryptocurrency;

namespace TokenVault.Api.Controllers;

public class CryptocurrenciesController : ApiController
{
    private readonly ICryptocurrencyRepository _cryptocurrencyRepository;
    private readonly ISender _mediatr;
    private readonly IMapper _mapper;

    public CryptocurrenciesController(
        ICryptocurrencyRepository cryptocurrencyRepository,
        ISender mediatr,
        IMapper mapper)
    {
        _cryptocurrencyRepository = cryptocurrencyRepository;
        _mediatr = mediatr;
        _mapper = mapper;
    }

    /// <summary>
    /// create cryptocurrency 
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateCryptocurrency([FromBody] CreateCryptocurrencyRequest request)
    {
        var command = _mapper.Map<CreateCryptocurrencyCommand>(request);
        var result = await _mediatr.Send(command);

        var response = _mapper.Map<CryptocurrencyResponse>(result);
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

        var response = _mapper.Map<CryptocurrencyResponse>(result);
        return Ok(response);
    }

    /// <summary>
    /// delete cryptocurrency 
    /// </summary>
    [HttpDelete("{cryptocurrencyId}")]
    public async Task<IActionResult> DeleteCryptocurrency([FromRoute] Guid cryptocurrencyId)
    {
        var command = new DeleteCryptocurrencyCommand(cryptocurrencyId);
        var result = await _mediatr.Send(command);

        var response = _mapper.Map<CryptocurrencyResponse>(result);
        return Ok(response);
    }

    /// <summary>
    /// get cryptocurrencies 
    /// </summary>
    [HttpGet]
    public IActionResult GetCryptocurrencies()
    {
        var cryptocurrencies = _cryptocurrencyRepository.GetCryptocurrenciesAsync();

        return Ok(cryptocurrencies);
    }
}