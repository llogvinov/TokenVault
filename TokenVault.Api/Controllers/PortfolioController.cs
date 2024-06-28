using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Application.Features.PortfolioAssets.Common;
using TokenVault.Application.Features.PortfolioAssets.Queries.GetPortfolioAssetsByPortfolioId;
using TokenVault.Application.Features.Portfolios.Commands.CreatePortfolio;
using TokenVault.Application.Features.Portfolios.Commands.DeletePortfolio;
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
    /// get portfolios
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetPortfolios()
    {
        var userId = GetUserId();
        if (userId == default)
        {
            return Unauthorized();
        }

        var response = await _unitOfWork.Portfolio.GetFirstOrDefaultAsync(p => p.UserId == userId);
        return Ok(response);
    }

    /// <summary>
    /// create portfolio
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreatePortfolio([FromBody] CreatePortfolioRequest request)
    {
        var userId = GetUserId();
        if (userId == default)
        {
            return Unauthorized();
        }

        var command = _mapper.Map<CreatePortfolioCommand>((userId, request));
        var portfolioResult = await _mediatr.Send(command);
        _unitOfWork.Save();

        var response = _mapper.Map<PortfolioResponse>(portfolioResult);
        return Ok(response);
    }

    /// <summary>
    /// get portfolio
    /// </summary>
    [HttpGet("{portfolioId}")]
    public async Task<IActionResult> GetPortfolio([FromRoute] Guid portfolioId)
    {
        var userId = GetUserId();
        if (userId == default)
        {
            return Unauthorized();
        }

        var query = new GetPortfolioByIdQuery(portfolioId);
        var portfolioResult = await _mediatr.Send(query);

        var getPortfolioAssetsQuery = new GetPortfolioAssetsByPortfolioIdQuery(portfolioId);
        var portfolioAssetsResult = await _mediatr.Send(getPortfolioAssetsQuery);

        var response = new PortfolioDetailedResponse(portfolioResult, portfolioAssetsResult);
        return Ok(response);
    }

    /// <summary>
    /// delete portfolio
    /// </summary>
    [HttpDelete("{portfolioId}")]
    public async Task<IActionResult> DeletePortfolio([FromRoute] Guid portfolioId)
    {
        var userId = GetUserId();
        if (userId == default)
        {
            return Unauthorized();
        }

        var command = new DeletePortfolioCommand(portfolioId);
        var portfolioResult = await _mediatr.Send(command);
        _unitOfWork.Save();

        var response = _mapper.Map<PortfolioResponse>(portfolioResult);
        return Ok(response);
    }
}

public record PortfolioDetailedResponse(
    PortfolioResult PortfolioResult,
    IEnumerable<PortfolioAssetResult> PortfolioAssetsResult);