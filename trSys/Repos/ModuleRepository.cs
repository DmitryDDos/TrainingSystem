using Microsoft.EntityFrameworkCore;
using trSys.Interfaces;
using trSys.Models;
using trSys.Data;
using trSys.Repos;

namespace trSys.Repositories;

public class ModuleRepository : BaseRepository<Module>, IModuleRepository
{
    public ModuleRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<Module>> GetByCourseIdAsync(int courseId)
        => await _context.Modules
            .Where(m => m.CourseId == courseId)
            .Include(m => m.Lessons)
            .ToListAsync();

    public async Task<bool> ExistsAsync(int id)
        => await _context.Modules.AnyAsync(m => m.Id == id);
}