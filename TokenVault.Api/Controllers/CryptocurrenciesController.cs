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
    ///     Get all cryptocurrencies
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetCryptocurrenciesAsync()
    {
        var cryptocurrencies = await _unitOfWork.Cryptocurrency.GetAllAsync();
        return Ok(cryptocurrencies);
    }
    
    /// <summary>
    ///     Get cryptocurrency by id
    /// </summary>
    /// <param name="cryptocurrencyId">Id of cryptocurrency</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{cryptocurrencyId}")]
    public async Task<IActionResult> GetCryptocurrencyAsync(
        [FromRoute] Guid cryptocurrencyId,
        CancellationToken cancellationToken = default)
    {
        var command = new GetCryptocurrencyByIdQuery(cryptocurrencyId);
        var result = await _mediatr.Send(command, cancellationToken);

        var response = _mapper.Map<CryptocurrencyResponse>(result);
        return Ok(response);
    }

    /// <summary>
    ///     Create cryptocurrency
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CreateCryptocurrencyAsync(
        [FromBody] CreateCryptocurrencyRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = _mapper.Map<CreateCryptocurrencyCommand>(request);
        var result = await _mediatr.Send(command, cancellationToken);
        await _unitOfWork.SaveAsync();

        var response = _mapper.Map<CryptocurrencyResponse>(result);
        return Ok(response);
    }

    /// <summary>
    ///     Update cryptocurrency
    /// </summary>
    /// <param name="cryptocurrencyId">Id of cryptocurrency</param>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut("{cryptocurrencyId}")]
    public async Task<IActionResult> UpdateCryptocurrencyAsync(
        [FromRoute] Guid cryptocurrencyId,
        [FromBody] UpdateCryptocurrencyRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = new UpdateCryptocurrencyCommand(cryptocurrencyId, request);
        var result = await _mediatr.Send(command, cancellationToken);
        await _unitOfWork.SaveAsync();

        var response = _mapper.Map<CryptocurrencyResponse>(result);
        return Ok(response);
    }

    /// <summary>
    ///     Delete cryptocurrency
    /// </summary>
    /// <param name="cryptocurrencyId">Id of cryptocurrency</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete("{cryptocurrencyId}")]
    public async Task<IActionResult> DeleteCryptocurrencyAsync(
        [FromRoute] Guid cryptocurrencyId,
        CancellationToken cancellationToken = default)
    {
        var command = new DeleteCryptocurrencyCommand(cryptocurrencyId);
        var result = await _mediatr.Send(command, cancellationToken);
        await _unitOfWork.SaveAsync();

        var response = _mapper.Map<CryptocurrencyResponse>(result);
        return Ok(response);
    }
}