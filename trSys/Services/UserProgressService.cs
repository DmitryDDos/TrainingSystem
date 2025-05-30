using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using trSys.DTOs;
using trSys.Interfaces;
using trSys.Models;

namespace trSys.Services;

public class UserProgressService : IUserProgressService
{
    private readonly IUserProgressRepository _progressRepo;
    private readonly ICourseRegistrationRepository _registrationRepo;
    private readonly IModuleRepository _moduleRepo;
    private readonly ITestRepository _testRepo;

    public UserProgressService(
        IUserProgressRepository progressRepo,
        ICourseRegistrationRepository registrationRepo,
        IModuleRepository moduleRepo,
        ITestRepository testRepo)  // Добавлен новый репозиторий
    {
        _progressRepo = progressRepo;
        _registrationRepo = registrationRepo;
        _moduleRepo = moduleRepo;
        _testRepo = testRepo;  // Инициализация нового репозитория
    }

    public async Task<UserProgressDto> GetProgressAsync(int userId, int courseId)
    {
        if (!await _registrationRepo.HasAccessAsync(userId, courseId))
            return null;

        var progress = await _progressRepo.GetByUserAndCourseAsync(userId, courseId);
        int totalModules = await _moduleRepo.GetCountByCourseIdAsync(courseId);
        var completedTests = progress?.CompletedTests ?? new List<int>();

        return progress == null
            ? new UserProgressDto(userId, courseId, 0, totalModules, completedTests)
            : new UserProgressDto(
                userId,
                courseId,
                progress.CompletedModules,
                totalModules,
                completedTests);  // Добавлен параметр CompletedTests
    }

    public async Task UpdateProgressAsync(int userId, int courseId, int completedModules)
    {
        if (!await _registrationRepo.HasAccessAsync(userId, courseId))
            throw new Exception("Пользователь не зарегистрирован на курс");

        await _progressRepo.UpdateProgressAsync(userId, courseId, completedModules);
    }

    public async Task<bool> HasAccessAsync(int userId, int courseId)
    {
        return await _registrationRepo.HasAccessAsync(userId, courseId);
    }

    public async Task CompleteModuleAsync(int userId, int courseId, int moduleId)
    {
        var module = await _moduleRepo.GetByIdAsync(moduleId);
        if (module == null) return;

        var progress = await _progressRepo.GetByUserAndCourseAsync(userId, courseId);
        int newCompletedModules = Math.Max(progress?.CompletedModules ?? 0, module.Order);

        await _progressRepo.UpdateProgressAsync(userId, courseId, newCompletedModules);
    }

    // Реализация новых методов интерфейса
    public async Task RecordTestAttempt(int userId, int testId, bool isPassed, int score)
    {
        if (isPassed)
        {
            await _progressRepo.AddCompletedTestAsync(userId, testId);
        }
    }

    public async Task<bool> AllModuleTestsPassed(int userId, int moduleId)
    {
        var moduleTests = await _testRepo.GetTestsByModuleIdAsync(moduleId);
        var completedTests = await _progressRepo.GetCompletedTestsAsync(userId);

        return moduleTests.All(t => completedTests.Contains(t.Id));
    }
}
