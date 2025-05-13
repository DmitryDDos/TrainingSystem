using System.ComponentModel.DataAnnotations;

namespace trSys.DTOs
{
    public record RegisterDto(
        [Required][EmailAddress] string Email,
        [Required][MinLength(6)] string Password,
        [Required] string FullName,
        string? Role = "User");
}
