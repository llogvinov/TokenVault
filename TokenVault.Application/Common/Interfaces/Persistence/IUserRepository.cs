using TokenVault.Domain.Entities;

namespace TokenVault.Application.Common.Interfaces.Persistence;

public interface IUserRepository
{
    void Add(User user);

    User? GetUserByEmail(string email);
}