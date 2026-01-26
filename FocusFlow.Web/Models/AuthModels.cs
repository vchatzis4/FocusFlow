namespace FocusFlow.Web.Models
{
    public record RegisterRequest(
        string UserName,
        string Email,
        string Password,
        string? FirstName,
        string? LastName
    );

    public record LoginRequest(
        string UserName,
        string Password
    );

    public record AuthResponse(
        string UserId,
        string UserName,
        string Email,
        string Token,
        DateTime Expiration
    );

    public record UserInfo(
        string Id,
        string UserName,
        string Email,
        string? FirstName,
        string? LastName
    );
}
