using System.ComponentModel.DataAnnotations;

namespace trSys.Models
{
    public class RegisterModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string Role { get; set; }
    }
}
