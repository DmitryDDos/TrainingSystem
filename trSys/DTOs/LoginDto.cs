using System.ComponentModel.DataAnnotations;

namespace trSys.DTOs
{
    public record LoginDto(
        [Required][EmailAddress] string Email,
        [Required] string Password);
}
