using trSys.DTOs;

namespace trSys.Interfaces;

public interface IUserProgressService
{
    Task<UserProgressDto> GetProgressAsync(int userId, int courseId);
    Task UpdateProgressAsync(int userId, int courseId, int completedModules);
    Task<bool> HasAccessAsync(int userId, int courseId);
    Task CompleteModuleAsync(int userId, int courseId, int moduleId);
    Task RecordTestAttempt(int userId, int testId, bool isPassed, int score);
    Task<bool> AllModuleTestsPassed(int userId, int moduleId);
}
