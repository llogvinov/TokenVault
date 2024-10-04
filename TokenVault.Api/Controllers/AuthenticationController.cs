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
    private readonly ISender _mediatr;
    private readonly IMapper _mapper;

    public AuthenticationController(
        IUnitOfWork unitOfWork,
        ISender mediatr,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mediatr = mediatr;
        _mapper = mapper;
    }

    /// <summary>
    ///     Register in system
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <response code="201">Registers the user to the system, logins the user and generates jwt token</response>
    /// <response code="400">If the user data is incorrect</response>
    [HttpPost("register")]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Register(
        RegisterRequest request,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var command = _mapper.Map<RegisterCommand>(request);
            var authResult = await _mediatr.Send(command, cancellationToken);
            await _unitOfWork.SaveAsync();
    
            var response = _mapper.Map<AuthenticationResponse>(authResult);
            return Ok(response);
        }
        catch (ArgumentException)
        {
            return BadRequest();
        }
    }

    /// <summary>
    ///     Login to system
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <response code="201">Logins the user to the system and generates jwt token</response>
    /// <response code="400">If the user data is incorrect</response>
    [HttpPost("login")]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Login(
        LoginRequest request,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var query = _mapper.Map<LoginQuery>(request);
            var authResult = await _mediatr.Send(query, cancellationToken);

            var response = _mapper.Map<AuthenticationResponse>(authResult);
            return Ok(response);
        }
        catch (ArgumentException)
        {
            return BadRequest();
        }
    }
}