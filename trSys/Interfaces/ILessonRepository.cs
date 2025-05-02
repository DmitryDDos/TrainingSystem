using trSys.Models;

namespace trSys.Interfaces;

public interface ILessonRepository : IRepository<Lesson>
{
    Task<IEnumerable<Lesson>> GetByModuleIdAsync(int moduleId);
}