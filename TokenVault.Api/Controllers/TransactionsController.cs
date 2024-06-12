using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        var command = _mapper.Map<CreateTransactionCommand>((request, portfolioId));
        var transaction = await _mediator.Send(command);
        
        var response = _mapper.Map<CreateTransactionResponse>(transaction);

        return Ok(response);
    }

    [HttpGet("{portfolioId}")]
    public IActionResult GetTransactions(Guid portfolioId)
    {
        var transactions = _transactionsService.GetTransactions(portfolioId);

        return Ok(transactions);
    }
}