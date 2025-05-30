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

    public async Task<AuthDto> RegisterUserAsync(RegisterDto dto, string adminId)
    {
        try
        {
            if (await _userRepo.ExistsAsync(dto.Email))
                return new AuthDto(false, "Пользователь с таким email уже существует");

            // Хеширование пароля перед созданием объекта
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            // Используем публичный конструктор
            var user = new User(
                email: dto.Email,
                pass: passwordHash, // Используем хешированный пароль
                name: dto.FullName,
                role: dto.Role
            );

            // Сохранение в БД
            await _userRepo.AddAsync(user);

            // Создание DTO для ответа
            var userDto = new UserDto(
                user.Id, // Преобразуем int в string
                user.Email,
                user.FullName,
                user.Role
            );

            return new AuthDto(true, "Пользователь успешно зарегистрирован", userDto);
        }
        catch (Exception ex)
        {
            // Раскомментируйте если есть _logger
            // _logger.LogError(ex, "Ошибка при регистрации пользователя");
            return new AuthDto(false, "Ошибка при регистрации пользователя");
        }
    }
}