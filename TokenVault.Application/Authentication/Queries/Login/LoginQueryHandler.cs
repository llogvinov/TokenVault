using MediatR;
using TokenVault.Application.Authentication.Common;
using TokenVault.Application.Common.Interfaces.Authentication;
using TokenVault.Application.Common.Interfaces.Persistence;

namespace TokenVault.Application.Authentication.Queries.Login;

public class LoginQueryHandler : IRequestHandler<LoginQuery, AuthenticationResult>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUnitOfWork _unitOfWork;

    public LoginQueryHandler(
        IJwtTokenGenerator jwtTokenGenerator,
        IUnitOfWork unitOfWork)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _unitOfWork = unitOfWork;
    }

    public async Task<AuthenticationResult> Handle(
        LoginQuery query,
        CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.User.GetFirstOrDefaultAsync(
            u => u.Email == query.Email);
        if (user is null)
        {
            throw new Exception("The user does not exist");
        }

        var hasher = new Hasher();
        var hashedPassword = hasher.ComputeSha256Hash(query.Password);
        if (user.Password != hashedPassword)
        {
            throw new Exception("The password is incorrect");
        }

        var token = _jwtTokenGenerator.GenerateToken(user);

        var result = new AuthenticationResult(user, token);
        return result;
    }
}