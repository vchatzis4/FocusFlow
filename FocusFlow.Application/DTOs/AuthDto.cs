namespace FocusFlow.Application.DTOs
{
    public record RegisterDto(
        string Email,
        string Password,
        string? FirstName,
        string? LastName
    );

    public record LoginDto(
        string Email,
        string Password
    );

    public record AuthResponseDto(
        string UserId,
        string Email,
        string Token,
        DateTime Expiration
    );

    public record UserDto(
        string Id,
        string Email,
        string? FirstName,
        string? LastName
    );
}
