using Microsoft.EntityFrameworkCore;
using trSys.Data;
using trSys.DTOs;
using trSys.Interfaces;
using trSys.Models;

namespace trSys.Repos;

public class CourseRegistrationRepository : BaseRepository<CourseRegistration>
{
    private readonly AppDbContext _context;

    public CourseRegistrationRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<CourseRegistration> RegisterUser(int userId, int courseId)
    {
        if (await _context.CourseRegistrations
            .AnyAsync(cr => cr.UserId == userId && cr.CourseId == courseId))
        {
            throw new Exception("Пользователь уже зарегистрирован на курс");
        }

        var registration = new CourseRegistration(0, userId, courseId);
        await AddAsync(registration); // Используем метод из BaseRepository
        return registration;
    }

    public async Task<bool> HasAccess(int userId, int courseId)
    {
        return await _context.CourseRegistrations
            .AnyAsync(cr => cr.UserId == userId && cr.CourseId == courseId);
    }

    public async Task<IEnumerable<Course>> GetUserCourses(int userId)
    {
        return await _context.CourseRegistrations
            .Where(cr => cr.UserId == userId)
            .Include(cr => cr.Course!)  // Явная подгрузка курса
            .ThenInclude(c => c.Registrations)  // Подгрузка регистраций курса
            .Select(cr => cr.Course)
            .ToListAsync();
    }


    public async Task<int> GetRegistrationCount(int courseId)
    {
        return await _context.CourseRegistrations
            .CountAsync(cr => cr.CourseId == courseId);
    }
}
