using trSys.DTOs;
using trSys.Interfaces;
using trSys.Models;

namespace trSys.Services;

public class UserProgressService : IUserProgressService
{
    private readonly IUserProgressRepository _progressRepo;
    private readonly ICourseRegistrationRepository _registrationRepo;

    public UserProgressService(
        IUserProgressRepository progressRepo,
        ICourseRegistrationRepository registrationRepo)
    {
        _progressRepo = progressRepo;
        _registrationRepo = registrationRepo;
    }

    public async Task<UserProgressDto> GetProgressAsync(int userId, int courseId)
    {
        if (!await _registrationRepo.HasAccessAsync(userId, courseId))
            return null;

        var progress = await _progressRepo.GetByUserAndCourseAsync(userId, courseId);

        return progress == null
            ? new UserProgressDto(userId, courseId, 0, 0)
            : new UserProgressDto(
                userId,
                courseId,
                progress.CompletedModules,
                progress.Course?.Modules.Count ?? 0);
    }

    public async Task UpdateProgressAsync(int userId, int courseId, int completedModules)
    {
        if (!await _registrationRepo.HasAccessAsync(userId, courseId))
            throw new Exception("Пользователь не зарегистрирован на курс");

        await _progressRepo.UpdateProgressAsync(userId, courseId, completedModules);
    }
}
