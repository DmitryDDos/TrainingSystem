using trSys.Models;

namespace trSys.Interfaces
{
    public interface IQuestionRepository : IRepository<Question>
    {
        Task<List<Question>> GetByTestIdAsync(int testId);
    }
}
