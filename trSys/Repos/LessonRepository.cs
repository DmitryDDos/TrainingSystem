using Microsoft.EntityFrameworkCore;
using trSys.Interfaces;
using trSys.Data;
using trSys.Models;
using trSys.Repos;

namespace trSys.Repositories;

public class LessonRepository : BaseRepository<Lesson>, ILessonRepository
{
    public LessonRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<Lesson>> GetByModuleIdAsync(int moduleId)
        => await _context.Lessons
            .Where(l => l.ModuleId == moduleId)
            .Include(l => l.Files)
            .ToListAsync();
}