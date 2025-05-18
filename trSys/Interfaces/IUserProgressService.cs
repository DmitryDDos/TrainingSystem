using trSys.DTOs;

namespace trSys.Interfaces;

public interface IUserProgressService
{
    Task<UserProgressDto> GetProgressAsync(int userId, int courseId);
    Task UpdateProgressAsync(int userId, int courseId, int completedModules);
}
