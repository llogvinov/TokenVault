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

    /// <summary>
    ///     Register in system
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("register")]
    public async Task<IActionResult> Register(
        RegisterRequest request, 
        CancellationToken cancellationToken = default)
    {
        var command = _mapper.Map<RegisterCommand>(request);
        var authResult = await _mediator.Send(command, cancellationToken);
        await _unitOfWork.SaveAsync();
        
        var response = _mapper.Map<AuthenticationResponse>(authResult);
        return Ok(response);
    }

    /// <summary>
    ///     Login to system
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("login")]
    public async Task<IActionResult> Login(
        LoginRequest request,
        CancellationToken cancellationToken = default)
    {
        var query = _mapper.Map<LoginQuery>(request);
        var authResult = await _mediator.Send(query, cancellationToken);

        var response = _mapper.Map<AuthenticationResponse>(authResult);
        return Ok(response);
    }
}