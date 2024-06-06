using TokenVault.Domain.Entities;

namespace TokenVault.Application.Services.Authentication;

public record AuthenticationResult(
    User User,
    string Token);