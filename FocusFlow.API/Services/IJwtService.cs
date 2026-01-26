using FocusFlow.Infrastructure.Identity;

namespace FocusFlow.API.Services
{
    public interface IJwtService
    {
        string GenerateToken(ApplicationUser user);
        DateTime GetExpiration();
    }
}
