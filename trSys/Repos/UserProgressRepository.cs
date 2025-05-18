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
}
