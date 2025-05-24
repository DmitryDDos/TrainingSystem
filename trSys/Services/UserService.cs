using trSys.DTOs;
using trSys.Interfaces;
using trSys.Mappers;
using trSys.Models;
using trSys.Repos;

namespace trSys.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepo;
    private readonly IPasswordHasher _passwordHasher;

    public UserService(
        IUserRepository userRepo,
        IPasswordHasher passwordHasher)
    {
        _userRepo = userRepo;
        _passwordHasher = passwordHasher;
    }

    public async Task<AuthDto> LoginAsync(LoginDto dto)
    {
        var user = await _userRepo.GetByEmailAsync(dto.Email);
        if (user == null || !_passwordHasher.VerifyHash(dto.Password, user.PasswordHash))
            return new AuthDto(false, "Неверные учетные данные");

        return new AuthDto(
            true,
            "Успешный вход",
            UserMapper.ToDto(user));
    }
}