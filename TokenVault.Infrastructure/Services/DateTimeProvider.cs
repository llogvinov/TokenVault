using TokenVault.Application.Common.Interfaces.Services;

namespace TokenVault.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}