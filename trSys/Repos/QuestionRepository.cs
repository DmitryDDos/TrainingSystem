using Microsoft.EntityFrameworkCore;
using trSys.Data;
using trSys.Interfaces;
using trSys.Models;

namespace trSys.Repos
{
    public class QuestionRepository : BaseRepository<Question>, IQuestionRepository
    {
        public QuestionRepository(AppDbContext context) : base(context) { }

        public async Task<List<Question>> GetByTestIdAsync(int testId)
        {
            return await _context.Questions
                .Include(q => q.Answers)
                .Where(q => q.TestId == testId)
                .ToListAsync();
        }
    }
}
