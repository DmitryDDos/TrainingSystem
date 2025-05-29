using Microsoft.EntityFrameworkCore;
using trSys.Data;
using trSys.Interfaces;
using trSys.Models;
using trSys.Repos;

namespace trSys.Repositories;

public class ModuleRepository : BaseRepository<Module>, IModuleRepository
{
    public ModuleRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<Module>> GetByCourseIdAsync(int courseId)
        => await _context.Modules
            .Where(m => m.CourseId == courseId)
            .AsNoTracking()
            .ToListAsync();

    public async Task<Module?> GetWithLessonsAsync(int id)
        => await _context.Modules
            .Include(m => m.Lessons)
            .Include(m => m.Tests)
            .FirstOrDefaultAsync(m => m.Id == id);

    //public async Task<Module?> GetWithTestsAsync(int id)
    //    => await _context.Modules
    //        .Include(m => m.Tests)
    //        .FirstOrDefaultAsync(m => m.Id == id);
}
