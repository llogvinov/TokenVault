using TokenVault.Application.Common.Interfaces.Authentication;

namespace TokenVault.Application.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public AuthenticationResult Register(string name, string email, string password)
    {
        // check if user already exists

        // create user (generate unique id)

        // create jwt token
        Guid userId = Guid.NewGuid();
        var token = _jwtTokenGenerator.GenerateToken(userId, name);
        
        return new AuthenticationResult(
            userId,
            name,
            email,
            token);
    }
    
    public AuthenticationResult Login(string email, string password)
    {
        return new AuthenticationResult(
            Guid.NewGuid(),
            "name",
            email,
            "token");
    }
}