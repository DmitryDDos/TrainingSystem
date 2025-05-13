namespace trSys.Models
{
    public record AuthResult(
        bool Success,
        string Message,
        string? Token = null);
}
