using FocusFlow.Web.Models;

namespace FocusFlow.Web.Services
{
    public interface IAuthService
    {
        Task<AuthResponse?> RegisterAsync(RegisterRequest request);
        Task<AuthResponse?> LoginAsync(LoginRequest request);
        Task LogoutAsync();
        Task<UserInfo?> GetCurrentUserAsync();
    }
}
