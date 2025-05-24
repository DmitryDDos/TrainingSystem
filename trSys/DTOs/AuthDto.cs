namespace trSys.DTOs
{
    public record AuthDto(
        bool Success,
        string Message,
        UserDto? User = null);
}
