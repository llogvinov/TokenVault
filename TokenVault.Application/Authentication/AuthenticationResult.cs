namespace TokenVault.Application.Authentication;

public record AuthenticationResult(
    Guid Id,
    string Name,
    string Email,
    string Token);