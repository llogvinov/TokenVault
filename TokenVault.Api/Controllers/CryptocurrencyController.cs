using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Application.Features.Cryptocurrencies.Commands.CreateCryptocurrency;
using TokenVault.Application.Features.Cryptocurrencies.Commands.DeleteCryptocurrency;
using TokenVault.Application.Features.Cryptocurrencies.Commands.UpdateCryptocurrency;
using TokenVault.Application.Features.Cryptocurrencies.Queries.GetCryptocurrencyById;
using TokenVault.Contracts.Cryptocurrency;

namespace TokenVault.Api.Controllers;

public class CryptocurrenciesController : ApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISender _mediatr;
    private readonly IMapper _mapper;

    public CryptocurrenciesController(
        IUnitOfWork unitOfWork,
        ISender mediatr,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mediatr = mediatr;
        _mapper = mapper;
    }

    /// <summary>
    /// create cryptocurrency 
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateCryptocurrencyAsync([FromBody] CreateCryptocurrencyRequest request)
    {
        var command = _mapper.Map<CreateCryptocurrencyCommand>(request);
        var result = await _mediatr.Send(command);
        await _unitOfWork.SaveAsync();

        var response = _mapper.Map<CryptocurrencyResponse>(result);
        return Ok(response);
    }
    
    /// <summary>
    /// get cryptocurrency by id  
    /// </summary>
    [HttpGet("{cryptocurrencyId}")]
    public async Task<IActionResult> GetCryptocurrencyAsync([FromRoute] Guid cryptocurrencyId)
    {
        var command = new GetCryptocurrencyByIdQuery(cryptocurrencyId);
        var result = await _mediatr.Send(command);

        var response = _mapper.Map<CryptocurrencyResponse>(result);
        return Ok(response);
    }

    /// <summary>
    /// get cryptocurrency by id  
    /// </summary>
    [HttpPut("{cryptocurrencyId}")]
    public async Task<IActionResult> UpdateCryptocurrencyAsync(
        [FromRoute] Guid cryptocurrencyId,
        [FromBody] UpdateCryptocurrencyRequest request)
    {
        var command = new UpdateCryptocurrencyCommand(cryptocurrencyId, request);
        var result = await _mediatr.Send(command);
        await _unitOfWork.SaveAsync();

        var response = _mapper.Map<CryptocurrencyResponse>(result);
        return Ok(response);
    }

    /// <summary>
    /// delete cryptocurrency 
    /// </summary>
    [HttpDelete("{cryptocurrencyId}")]
    public async Task<IActionResult> DeleteCryptocurrencyAsync([FromRoute] Guid cryptocurrencyId)
    {
        var command = new DeleteCryptocurrencyCommand(cryptocurrencyId);
        var result = await _mediatr.Send(command);
        await _unitOfWork.SaveAsync();

        var response = _mapper.Map<CryptocurrencyResponse>(result);
        return Ok(response);
    }

    /// <summary>
    /// get cryptocurrencies 
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetCryptocurrencies()
    {
        var cryptocurrencies = await _unitOfWork.Cryptocurrency.GetAllAsync();
        return Ok(cryptocurrencies);
    }
}