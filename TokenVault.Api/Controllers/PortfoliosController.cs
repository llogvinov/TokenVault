using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Application.Features.PortfolioAssets.Common;
using TokenVault.Application.Features.PortfolioAssets.Queries.GetPortfolioAssetsByPortfolioId;
using TokenVault.Application.Features.Portfolios.Commands.CreatePortfolio;
using TokenVault.Application.Features.Portfolios.Commands.DeletePortfolio;
using TokenVault.Application.Features.Portfolios.Commands.UpdatePortfolio;
using TokenVault.Application.Features.Portfolios.Common;
using TokenVault.Application.Features.Portfolios.Queries.GetPortfolioById;
using TokenVault.Contracts.Portfolio;

namespace TokenVault.Api.Controllers;

public class PortfoliosController : ApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISender _mediatr;
    private readonly IMapper _mapper;

    public PortfoliosController(
        IUnitOfWork unitOfWork,
        ISender mediatr,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mediatr = mediatr;
        _mapper = mapper;
    }

    /// <summary>
    ///     Get all portfolios
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetPortfoliosAsync()
    {
        var userId = GetUserId();

        var response = await _unitOfWork.Portfolio.GetPortfoliosByUserIdAsync(userId);
        return Ok(response);
    }

    /// <summary>
    ///     Get portfolio by id
    /// </summary>
    /// <param name="portfolioId">Id of portfolio</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{portfolioId}")]
    public async Task<IActionResult> GetPortfolioAsync(
        [FromRoute] Guid portfolioId,
        CancellationToken cancellationToken = default)
    {
        var query = new GetPortfolioByIdQuery(portfolioId);
        var portfolioResult = await _mediatr.Send(query, cancellationToken);

        var getPortfolioAssetsQuery = new GetPortfolioAssetsByPortfolioIdQuery(portfolioId);
        var portfolioAssetsResult = await _mediatr.Send(getPortfolioAssetsQuery, cancellationToken);

        var response = new PortfolioDetailedResponse(portfolioResult, portfolioAssetsResult);
        return Ok(response);
    }

    /// <summary>
    ///     Crete portfolio
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CreatePortfolioAsync(
        [FromBody] CreatePortfolioRequest request,
        CancellationToken cancellationToken = default)
    {
        var userId = GetUserId();

        var command = _mapper.Map<CreatePortfolioCommand>((userId, request));
        var portfolioResult = await _mediatr.Send(command, cancellationToken);
        await _unitOfWork.SaveAsync();

        var response = _mapper.Map<PortfolioResponse>(portfolioResult);
        return CreatedAtAction(nameof(GetPortfolioAsync),
            new { portfolioId = response.Id },
            response);
    }

    /// <summary>
    ///     Update portfolio
    /// </summary>
    /// <param name="portfolioId">Id of portfolio</param>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut("{portfolioId}")]
    public async Task<IActionResult> UpdatePortfolioAsync(
        [FromRoute] Guid portfolioId,
        [FromBody] UpdatePortfolioRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = new UpdatePortfolioCommand(portfolioId, request.Title);
        var portfolioResult = await _mediatr.Send(command, cancellationToken);
        await _unitOfWork.SaveAsync();

        var response = _mapper.Map<PortfolioResponse>(portfolioResult);
        return NoContent();
    }

    /// <summary>
    ///     Delete portfolio
    /// </summary>
    /// <param name="portfolioId">Id of portfolio</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete("{portfolioId}")]
    public async Task<IActionResult> DeletePortfolioAsync(
        [FromRoute] Guid portfolioId,
        CancellationToken cancellationToken = default)
    {
        var command = new DeletePortfolioCommand(portfolioId);
        var portfolioResult = await _mediatr.Send(command, cancellationToken);
        await _unitOfWork.SaveAsync();

        var response = _mapper.Map<PortfolioResponse>(portfolioResult);
        return NoContent();
    }
}

public record PortfolioDetailedResponse(
    PortfolioResult PortfolioResult,
    IEnumerable<PortfolioAssetResult> PortfolioAssetsResult);