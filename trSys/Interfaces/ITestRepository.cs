using trSys.Models;
using trSys.Repos;

namespace trSys.Interfaces
{
    public interface ITestRepository : IRepository<Test>
    {
        Task<Test?> GetWithQuestionsAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<IEnumerable<Test>> GetByModuleIdAsync(int moduleId);
        Task<IEnumerable<Test>> GetTestsWithQuestionsByModuleIdAsync(int moduleId);
        Task<IEnumerable<Test>> GetTestsByModuleIdAsync(int moduleId);
    }
}
