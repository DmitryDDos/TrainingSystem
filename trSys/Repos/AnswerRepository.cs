using trSys.Data;
using trSys.Interfaces;
using trSys.Models;

namespace trSys.Repos;

public class AnswerRepository : BaseRepository<Answer>, IAnswerRepository
{
    public AnswerRepository(AppDbContext context) : base(context) { }
}
