using trSys.DTOs;
using trSys.Interfaces;
using trSys.Models;

namespace trSys.Mappers
{
    public static class UserMapper
    {
        public static UserDto ToDto(User user) => new(
            user.Id,
            user.Email,
            user.FullName,
            user.Role);

        public static User FromRegisterDto(RegisterDto dto, IPasswordHasher hasher) => new(
            dto.Email,
            hasher.CreateHash(dto.Password),
            dto.FullName,
            dto.Role ?? "User");
    }
}
