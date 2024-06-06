using MediatR;
using TokenVault.Application.Authentication.Common;

namespace TokenVault.Application.Authentication.Queries.Login;

public record LoginQuery(
    string Email,
    string Password) : IRequest<AuthenticationResult>;