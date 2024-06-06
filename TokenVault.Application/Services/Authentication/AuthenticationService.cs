using TokenVault.Application.Common.Interfaces.Authentication;
using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Domain.Entities;

namespace TokenVault.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public AuthenticationResult Register(string name, string email, string password)
    {
        // validate the user doesn't exist
        if (_userRepository.GetUserByEmail(email) is not null)
        {
            throw new Exception("User with given email already exists");
        }

        // create new user
        var user = new User
        {
            Name = name,
            Email = email,
            Password = password
        };
        _userRepository.Add(user);

        // create jwt token
        var token = _jwtTokenGenerator.GenerateToken(user);
        
        return new AuthenticationResult(
            user,
            token);
    }
    
    public AuthenticationResult Login(string email, string password)
    {
        // validate the user exists
        if (_userRepository.GetUserByEmail(email) is not User user)
        {
            throw new Exception("The user does not exist");
        }

        // validate the password is correct
        if (user.Password != password)
        {
            throw new Exception("The password is incorrect");
        }

        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(
            user,
            token);
    }
}