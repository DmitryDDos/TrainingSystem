using Microsoft.EntityFrameworkCore;
using System;
using trSys.Data;
using trSys.Interfaces;
using trSys.Models;

namespace trSys.Repos
{
    public class TestRepository : BaseRepository<Test>, ITestRepository
    {
        public TestRepository(AppDbContext context) : base(context) { }

        public async Task<Test?> GetWithQuestionsAsync(int id)
        {
            return await _context.Tests
                .Include(t => t.Questions)
                .ThenInclude(q => q.Answers)
                .FirstOrDefaultAsync(t => t.Id == id);
        }
    }
}
