using trSys.Models;

namespace trSys.Interfaces;

public interface IModuleRepository : IRepository<Module>
{
    Task<IEnumerable<Module>> GetByCourseIdAsync(int courseId);
    Task<bool> ExistsAsync(int id);
    Task<Module?> GetWithLessonsAsync(int id);
}
