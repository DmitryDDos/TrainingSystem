using System.ComponentModel.DataAnnotations;

namespace trSys.DTOs
{
    public record RegisterDto(
    [Required(ErrorMessage = "Email обязателен")]
    [EmailAddress(ErrorMessage = "Некорректный формат email")]
    string Email,

    [Required(ErrorMessage = "Полное имя обязательно")]
    string FullName,

    [Required(ErrorMessage = "Пароль обязателен")]
    [DataType(DataType.Password)]
    [MinLength(6, ErrorMessage = "Пароль должен содержать минимум 6 символов")]
    string Password,

    [Required(ErrorMessage = "Роль обязательна")]
    string Role = "User");
}
