using trSys.Models;
using trSys.DTOs;

namespace trSys.Interfaces;

public interface ICourseRegistrationRepository : IRepository<CourseRegistration>
{
    Task<CourseRegistration> RegisterUserAsync(int userId, int courseId);
    Task<bool> HasAccessAsync(int userId, int courseId);
    Task<IEnumerable<Course>> GetUserCoursesAsync(int userId);
    Task<int> GetRegistrationCountAsync(int courseId);
}
