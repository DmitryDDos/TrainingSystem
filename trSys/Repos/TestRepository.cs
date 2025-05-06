using Microsoft.EntityFrameworkCore;
using trSys.Data;
using trSys.Interfaces;
using trSys.Models;
using trSys.Repos;

namespace trSys.Repositories;

public class TestRepository : BaseRepository<Test>, ITestRepository
{
    public TestRepository(AppDbContext context) : base(context) { }

    public async Task<Test?> GetWithQuestionsAsync(int id)
        => await _context.Tests
            .Include(t => t.Questions)
                .ThenInclude(q => q.Answers)
            .FirstOrDefaultAsync(t => t.Id == id);

    public async Task<bool> ExistsAsync(int id)
    => await _context.Tests.AnyAsync(t => t.Id == id);

}
