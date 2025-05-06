using Microsoft.EntityFrameworkCore;
using trSys.Data;
using trSys.Interfaces;
using trSys.Models;

namespace trSys.Repos;

public class QuestionRepository : BaseRepository<Question>, IQuestionRepository
{
    public QuestionRepository(AppDbContext context) : base(context) { }

    //public async Task<IEnumerable<Question>> GetByTestIdWithAnswersAsync(int testId)
    //    => await _context.Questions
    //        .Include(q => q.Answers)
    //        .Where(q => q.TestId == testId)
    //        .ToListAsync();

    public async Task<List<Question>> GetByTestIdAsync(int testId)
    => await _context.Questions
        .Include(q => q.Answers)
        .Where(q => q.TestId == testId)
        .ToListAsync();


}
