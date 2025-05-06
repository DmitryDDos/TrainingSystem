using trSys.Models;

namespace trSys.Interfaces;

public interface ICourseRepository : IRepository<Course>
{
    Task<IEnumerable<Module>> GetCourseModulesAsync(int courseId);
    Task<bool> TitleExistsAsync(string title);
    Task<bool> ExistsAsync(int id);
}