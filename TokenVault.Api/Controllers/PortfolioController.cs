using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Application.Potfolio;
using TokenVault.Application.Potfolio.Commands.Create;
using TokenVault.Contracts.Portfolio;

namespace TokenVault.Api.Controllers;

public class PortfoliosController : ApiController
{
    private readonly IPortfolioRepository _portfoliosRepository;
    private readonly PortfolioService _portfolioService;
    private readonly ISender _mediatr;
    private readonly IMapper _mapper;

    public PortfoliosController(
        IPortfolioRepository portfoliosRepository,
        PortfolioService portfolioService,
        ISender mediatr,
        IMapper mapper)
    {
        _portfoliosRepository = portfoliosRepository;
        _portfolioService = portfolioService;
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

        var response = await _portfoliosRepository.GetPortfoliosAsync(userId);

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

        var command = new CreatePortfolioCommand(request.Title, userId);
        var createPortfolioResult = await _mediatr.Send(command);

        var response = new CreatePortfolioResponse(
            createPortfolioResult.PortfolioId,
            createPortfolioResult.UserId,
            createPortfolioResult.Title);

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

        var portfolio = await _portfoliosRepository.GetPortfolioByIdAsync(portfolioId);

        return Ok(portfolio);
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

        var response = await _portfolioService.DeletePortfolio(portfolioId);

        return Ok(response);
    }
}