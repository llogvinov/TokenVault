using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TokenVault.Api.Controllers;

[ApiController]
[Route("{controller}")]
[Authorize]
public class ApiController : ControllerBase
{
    protected Guid GetUserId()
    {
        var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub);
        if (userIdClaim != null)
        {
            Guid.TryParse(userIdClaim.Value, out var userId);
            return userId;
        }

        return default;
    }
}