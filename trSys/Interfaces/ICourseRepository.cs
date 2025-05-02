using trSys.Models;

namespace trSys.Interfaces;

public interface ICourseRepository : IRepository<Course>
{
    Task<bool> CourseExists(int id);
    Task<IEnumerable<Module>> GetModulesByCourseId(int courseId);
}