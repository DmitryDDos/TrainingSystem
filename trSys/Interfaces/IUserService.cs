using trSys.DTOs;

namespace trSys.Interfaces
{
    public interface IUserService
    {
        Task<AuthDto> LoginAsync(LoginDto dto);
        Task<AuthDto> RegisterUserAsync(RegisterDto dto, string adminId);
    }
}
