using Microsoft.AspNetCore.Mvc;
using TokenVault.Application.Transactions;
using TokenVault.Contracts.Transactions;

namespace TokenVault.Api.Controllers;

[Route("transactions")]
public class TransactionsController : ApiController
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
}