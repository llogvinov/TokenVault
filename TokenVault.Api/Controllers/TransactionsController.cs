using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using TokenVault.Application.Transactions;
using TokenVault.Contracts.Transactions;

namespace TokenVault.Api.Controllers;

[ApiController]
[Authorize]
[Route("transactions")]
public class TransactionsController : ControllerBase
{
    private TransactionsService _transactionsService;

    public TransactionsController(TransactionsService transactionsService)
    {
        _transactionsService = transactionsService;
    }

    [HttpPost("{portfolioId}/create")]
    public IActionResult CreateTransaction(CreateTransactionRequest request, Guid portfolioId)
    {
        var userId = GetUserId();
        if (userId == default)
        {
            return Unauthorized();
        }

        var response = _transactionsService.CreateTransaction(request, userId, portfolioId);

        return Ok(response);
    }

    [HttpGet]
    public IActionResult GetTransactionsByUserId()
    {
        var userId = GetUserId();
        if (userId == default)
        {
            return Unauthorized();
        }

        var transactions = _transactionsService.GetTransactionsByUserId(userId);

        return Ok(transactions);
    }

    [HttpGet("{portfolioId}")]
    public IActionResult GetTransactionsByPortfolioId(Guid portfolioId)
    {
        var transactions = _transactionsService.GetTransactionsByPortfolioId(portfolioId);

        return Ok(transactions);
    }

    private Guid GetUserId()
    {
        var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub);
        if (userIdClaim != null)
        {
            Guid.TryParse(userIdClaim.Value, out var userId);
            return userId;
        }

        return default;
    }
}