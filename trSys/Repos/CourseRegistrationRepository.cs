using Microsoft.EntityFrameworkCore;
using trSys.Data;
using trSys.DTOs;
using trSys.Interfaces;
using trSys.Models;

namespace trSys.Repos;

public class CourseRegistrationRepository : BaseRepository<CourseRegistration>, ICourseRegistrationRepository
{
    private readonly AppDbContext _context;

    public CourseRegistrationRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<CourseRegistration> RegisterUserAsync(int userId, int courseId)
    {
        if (await _context.CourseRegistrations
            .AnyAsync(cr => cr.UserId == userId && cr.CourseId == courseId))
        {
            throw new Exception("Пользователь уже зарегистрирован на курс");
        }

        var registration = new CourseRegistration(0, userId, courseId);
        await AddAsync(registration);
        return registration;
    }

    public async Task<bool> HasAccessAsync(int userId, int courseId)
        => await _context.CourseRegistrations
            .AnyAsync(cr => cr.UserId == userId && cr.CourseId == courseId);

    public async Task<IEnumerable<Course>> GetUserCoursesAsync(int userId)
        => await _context.CourseRegistrations
            .Where(cr => cr.UserId == userId)
            .Include(cr => cr.Course!)
            .ThenInclude(c => c.Registrations)
            .Select(cr => cr.Course)
            .ToListAsync();

    public async Task<int> GetRegistrationCountAsync(int courseId)
        => await _context.CourseRegistrations
            .CountAsync(cr => cr.CourseId == courseId);
}
