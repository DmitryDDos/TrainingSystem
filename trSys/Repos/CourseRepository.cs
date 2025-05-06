using Microsoft.EntityFrameworkCore;
using trSys.Data;
using trSys.Interfaces;
using trSys.Models;
using trSys.Repos;

namespace trSys.Repositories;

public class CourseRepository : BaseRepository<Course>, ICourseRepository
{
    public CourseRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<Module>> GetCourseModulesAsync(int courseId)
        => await _context.Modules
            .Where(m => m.CourseId == courseId)
            .AsNoTracking()
            .ToListAsync();

    public async Task<bool> TitleExistsAsync(string title)
        => await _context.Courses.AnyAsync(c => c.Title == title);
}