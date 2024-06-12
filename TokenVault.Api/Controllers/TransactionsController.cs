using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TokenVault.Contracts.Transactions;

namespace TokenVault.Api.Controllers;

[ApiController]
[Authorize]
[Route("transactions")]
public class TransactionsController : ControllerBase
{
    private ISender _mediator;
    private IMapper _mapper;

    public TransactionsController(ISender mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }
    
    [HttpPost("{{portfolioId}}/create")]
    public async Task<IActionResult> CreateTransactionAsync(CreateTransactionRequest request, Guid portfolioId)
    {
        var command = _mapper.Map<CreateTransactionCommand>((request, portfolioId));
        var transaction = await _mediator.Send(command);
        
        var response = _mapper.Map<CreateTransactionResponse>(transaction);

        return Ok(response);
    }

    [HttpGet("transactionId:guid")]
    public IActionResult GetTransactions(Guid transactionId)
    {
        return Ok(Array.Empty<string>());
    }
}