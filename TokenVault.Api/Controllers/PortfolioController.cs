using Microsoft.AspNetCore.Mvc;
using TokenVault.Application.Potfolio;

namespace TokenVault.Api.Controllers;

[Route("portfolio")]
public class PortfolioController : ApiController
{
    private readonly PortfolioService _portfolioService;

    public PortfolioController(PortfolioService portfolioService)
    {
        _portfolioService = portfolioService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreatePortfolio(CreatePortfolioRequest request)
    {
        var userId = GetUserId();
        if (userId == default)
        {
            return Unauthorized();
        }

        var response = await _portfolioService.CreatePortfolio(request, userId);

        return Ok(response);
    }
}