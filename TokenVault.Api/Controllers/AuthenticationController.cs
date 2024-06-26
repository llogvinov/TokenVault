using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TokenVault.Application.Authentication.Commands.Register;
using TokenVault.Application.Authentication.Queries.Login;
using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Contracts.Authentication;

namespace TokenVault.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthenticationController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public AuthenticationController(
        IUnitOfWork unitOfWork,
        ISender mediator,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = _mapper.Map<RegisterCommand>(request);
        var authResult = await _mediator.Send(command);
        await _unitOfWork.SaveAsync();
        
        var response = _mapper.Map<AuthenticationResponse>(authResult);
        return Ok(response);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var query = _mapper.Map<LoginQuery>(request);
        var authResult = await _mediator.Send(query);

        var response = _mapper.Map<AuthenticationResponse>(authResult);
        return Ok(response);
    }
}