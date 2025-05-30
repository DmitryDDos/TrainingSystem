using Microsoft.EntityFrameworkCore;
using trSys.Data;
using trSys.Interfaces;
using trSys.Models;
using trSys.Repos;

namespace trSys.Repositories;

public class ModuleRepository : BaseRepository<Module>, IModuleRepository
{
    public ModuleRepository(AppDbContext context) : base(context) { }

    public async Task<int> GetMaxOrderForCourseAsync(int courseId)
    {
        return await _context.Modules
            .Where(m => m.CourseId == courseId)
            .MaxAsync(m => (int?)m.Order) ?? 0;
    }

    public async Task<IEnumerable<Module>> GetByCourseIdAsync(int courseId)
        => await _context.Modules
            .Where(m => m.CourseId == courseId)
            .OrderBy(m => m.Order)
            .AsNoTracking()
            .ToListAsync();

    public async Task<Module?> GetWithLessonsAsync(int id)
    => await _context.Modules
        .Include(m => m.Lessons)
            .OrderBy(l => l.Order)
        .Include(m => m.Tests)
            .OrderBy(t => t.Order)
        .FirstOrDefaultAsync(m => m.Id == id);

    public async Task<int> GetCountByCourseIdAsync(int courseId)
    {
        return await _context.Modules
            .Where(m => m.CourseId == courseId)
            .CountAsync();
    }
}
