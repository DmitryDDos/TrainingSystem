using Microsoft.EntityFrameworkCore;
using trSys.Data;
using trSys.Interfaces;
using trSys.Models;

namespace trSys.Repos;

public class UserProgressRepository : IUserProgressRepository
{
    private readonly AppDbContext _context;

    public UserProgressRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<UserProgress?> GetByUserAndCourseAsync(int userId, int courseId)
    {
        return await _context.UserProgresses
            .Include(up => up.Course)
            .ThenInclude(c => c.Modules)
            .FirstOrDefaultAsync(p => p.UserId == userId && p.CourseId == courseId);
    }

    public async Task UpdateProgressAsync(int userId, int courseId, int completedModules)
    {
        var progress = await GetByUserAndCourseAsync(userId, courseId);

        if (progress == null)
        {
            progress = new UserProgress(userId, courseId, completedModules);
            _context.UserProgresses.Add(progress);
        }
        else
        {
            progress.UpdateProgress(completedModules);
        }

        await _context.SaveChangesAsync();
    }

    public async Task<int> GetCompletedModulesAsync(int userId, int courseId)
    {
        var progress = await GetByUserAndCourseAsync(userId, courseId);
        return progress?.CompletedModules ?? 0;
    }

    public async Task AddCompletedTestAsync(int userId, int testId)
    {
        var test = await _context.Tests
            .Include(t => t.Module)
            .FirstOrDefaultAsync(t => t.Id == testId);

        if (test == null) return;

        var progress = await GetByUserAndCourseAsync(userId, test.Module.CourseId);

        if (progress == null)
        {
            progress = new UserProgress(userId, test.Module.CourseId, 0);
            _context.UserProgresses.Add(progress);
        }

        if (!progress.CompletedTests.Contains(testId))
        {
            progress.CompletedTests.Add(testId);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<int>> GetCompletedTestsAsync(int userId)
    {
        return await _context.UserProgresses
            .Where(up => up.UserId == userId)
            .SelectMany(up => up.CompletedTests)
            .Distinct()
            .ToListAsync();
    }
}
