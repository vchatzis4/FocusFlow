namespace FocusFlow.Application.DTOs
{
    public record RegisterDto(
        string UserName,
        string Email,
        string Password,
        string? FirstName,
        string? LastName
    );

    public record LoginDto(
        string UserName,
        string Password
    );

    public record AuthResponseDto(
        string UserId,
        string UserName,
        string Email,
        string Token,
        DateTime Expiration
    );

    public record UserDto(
        string Id,
        string UserName,
        string Email,
        string? FirstName,
        string? LastName
    );
}
