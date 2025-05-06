using Microsoft.EntityFrameworkCore;
using trSys.Data;
using trSys.Interfaces;
using trSys.Models;
using trSys.Repos;

namespace trSys.Repositories;

public class LessonRepository : BaseRepository<Lesson>, ILessonRepository
{
    public LessonRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<Lesson>> GetByModuleIdAsync(int moduleId)
        => await _context.Lessons
            .Where(l => l.ModuleId == moduleId)
            .AsNoTracking()
            .ToListAsync();
}
