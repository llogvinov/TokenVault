using Microsoft.AspNetCore.Mvc;
using TokenVault.Application.Potfolio;

namespace TokenVault.Api.Controllers;

public class PortfolioController : ApiController
{
    private readonly PortfolioService _portfolioService;

    public PortfolioController(PortfolioService portfolioService)
    {
        _portfolioService = portfolioService;
    }

    /// <summary>
    /// create portfolio
    /// </summary>
    [HttpPost("create")]
    public async Task<IActionResult> CreatePortfolio([FromBody] CreatePortfolioRequest request)
    {
        var userId = GetUserId();
        if (userId == default)
        {
            return Unauthorized();
        }

        var response = await _portfolioService.CreatePortfolio(request, userId);

        return Ok(response);
    }

    /// <summary>
    /// delete portfolio
    /// </summary>
    [HttpDelete("delete/{portfolioId}")]
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