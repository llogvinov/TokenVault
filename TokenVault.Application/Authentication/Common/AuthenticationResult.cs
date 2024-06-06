using TokenVault.Domain.Entities;

namespace TokenVault.Application.Authentication.Common;

public record AuthenticationResult(
    User User,
    string Token);