using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using TokenVault.Application.Transactions;
using TokenVault.Application.Transactions.Commands.Create;
using TokenVault.Contracts.Transactions;

namespace TokenVault.Api.Controllers;

[ApiController]
[Authorize]
[Route("transactions")]
public class TransactionsController : ControllerBase
{
    private TransactionsService _transactionsService;
    private ISender _mediator;
    private IMapper _mapper;

    public TransactionsController(ISender mediator, IMapper mapper, TransactionsService transactionsService)
    {
        _mediator = mediator;
        _mapper = mapper;
        _transactionsService = transactionsService;
    }

    [HttpPost("{portfolioId}/create")]
    public async Task<IActionResult> CreateTransactionAsync(CreateTransactionRequest request, Guid portfolioId)
    {
        var userId = GetUserId();
        if (userId == default)
        {
            return Unauthorized();
        }

        var command = _mapper.Map<CreateTransactionCommand>((request, userId, portfolioId));
        var transaction = await _mediator.Send(command);
        
        var response = _mapper.Map<CreateTransactionResponse>(transaction);

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