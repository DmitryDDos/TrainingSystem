namespace trSys.DTOs
{
    public record AuthDto(
        bool Success,
        string Message,
        string? Token = null,
        UserDto? User = null);
}
