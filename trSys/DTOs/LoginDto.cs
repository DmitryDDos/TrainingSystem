using System.ComponentModel.DataAnnotations;

namespace trSys.DTOs
{
    public record LoginDto(
        [Required][EmailAddress] string Email,
        [Required][MinLength(6)] string Password,
        bool RememberMe
    );
}
