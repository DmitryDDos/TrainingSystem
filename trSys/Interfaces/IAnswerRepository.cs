using trSys.Models;

namespace trSys.Interfaces;

public interface IAnswerRepository : IRepository<Answer>
{
    Task<List<Answer>> GetByQuestionIdAsync(int questionId);
}
