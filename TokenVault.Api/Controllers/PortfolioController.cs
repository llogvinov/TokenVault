using System.IdentityModel.Tokens.Jwt;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TokenVault.Application.Potfolio.Commands.Create;
using TokenVault.Contracts.Portfolio;

namespace TokenVault.Api.Controllers;

[ApiController]
[Authorize]
[Route("portfolio")]
public class PortfolioController : ControllerBase
{
    private ISender _mediator;
    private IMapper _mapper;

    public PortfolioController(ISender mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }
    
    [HttpPost("create")]
    public async Task<IActionResult> CreatePortfolio(CreatePortfolioRequest request)
    {
        var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub);
        if (userIdClaim == null)
        {
            return Unauthorized();
        }

        Guid.TryParse(userIdClaim.Value, out var userId);

        var command = new CreatePortfolioCommand(request.Title, userId);
        var portfolio = await _mediator.Send(command);

        var response = _mapper.Map<CreatePortfolioResponse>(portfolio);

        return Ok(response);
    }
}