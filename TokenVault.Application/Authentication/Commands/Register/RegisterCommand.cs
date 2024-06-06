using MediatR;
using TokenVault.Application.Authentication.Common;

namespace TokenVault.Application.Authentication.Commands.Register;

public record RegisterCommand(
    string Name,
    string Email,
    string Password) : IRequest<AuthenticationResult>;