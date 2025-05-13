using trSys.DTOs;

namespace trSys.Interfaces
{
    public interface IUserService
    {
        Task<AuthDto> RegisterAsync(RegisterDto dto);
        Task<AuthDto> LoginAsync(LoginDto dto);
    }
}
