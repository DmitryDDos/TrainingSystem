using trSys.Models;

namespace trSys.Interfaces;

public interface IUserProgressRepository
{
    Task<UserProgress?> GetByUserAndCourseAsync(int userId, int courseId);
    Task UpdateProgressAsync(int userId, int courseId, int completedModules);
    Task<int> GetCompletedModulesAsync(int userId, int courseId);
}
