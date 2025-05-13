namespace trSys.DTOs
{
    public record UserDto(
        int Id,
        string Email,
        string FullName,
        string Role);
}
