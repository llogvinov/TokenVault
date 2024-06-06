using TokenVault.Domain.Entities;

namespace TokenVault.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}