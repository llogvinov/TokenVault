using Microsoft.AspNetCore.Mvc;
using TokenVault.Application.Transactions;
using TokenVault.Contracts.Transactions;

namespace TokenVault.Api.Controllers;

public class TransactionsController : ApiController
{
    private TransactionsService _transactionsService;

    public TransactionsController(TransactionsService transactionsService)
    {
        _transactionsService = transactionsService;
    }

    [HttpPost("{portfolioId}/create")]
    public async Task<IActionResult> CreateTransaction(
        [FromRoute] Guid portfolioId,
        [FromBody] CreateTransactionRequest request)
    {
        var userId = GetUserId();
        if (userId == default)
        {
            return Unauthorized();
        }

        var response = await _transactionsService.CreateTransactionAsync(request, userId, portfolioId);

        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetTransactionsByUserId()
    {
        var userId = GetUserId();
        if (userId == default)
        {
            return Unauthorized();
        }

        var transactions = await _transactionsService.GetTransactionsByUserIdAsync(userId);

        return Ok(transactions);
    }

    [HttpGet("{portfolioId}")]
    public async Task<IActionResult> GetTransactionsByPortfolioId([FromRoute] Guid portfolioId)
    {
        var transactions = await _transactionsService.GetTransactionsByPortfolioIdAsync(portfolioId);

        return Ok(transactions);
    }

    [HttpPost("{transactionId}/delete")]
    public async Task<IActionResult> DeleteTransaction([FromRoute] Guid transactionId)
    {
        var userId = GetUserId();
        if (userId == default)
        {
            return Unauthorized();
        }

        var response = await _transactionsService.DeleteTransactionAsync(userId, transactionId);

        return Ok(response);
    }
}