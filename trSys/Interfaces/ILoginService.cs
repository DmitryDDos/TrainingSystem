using trSys.DTOs;
using trSys.Models;

namespace trSys.Interfaces
{
    public interface ILoginService
    {
        Task<AuthResult> LoginAsync(LoginDto loginDto);
        Task LogoutAsync();
    }
}
