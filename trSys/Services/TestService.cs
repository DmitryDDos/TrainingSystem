using trSys.Interfaces;
using trSys.Models;
using trSys.Repos;

namespace trSys.Services
{
    public class TestService : ITestRepository
    {
        private readonly ITestRepository _testRepo;
        private readonly IQuestionRepository _questionRepo;

        public TestService(ITestRepository testRepo, IQuestionRepository questionRepo)
        {
            _testRepo = testRepo;
            _questionRepo = questionRepo;
        }

        // Реализация ITestRepository
        public async Task<IEnumerable<Test>> GetAllAsync() => await _testRepo.GetAllAsync();
        public async Task<Test> GetByIdAsync(int id) => await _testRepo.GetByIdAsync(id);
        public async Task AddAsync(Test entity) => await _testRepo.AddAsync(entity);
        public async Task UpdateAsync(Test entity) => await _testRepo.UpdateAsync(entity);
        public async Task DeleteAsync(int id) => await _testRepo.DeleteAsync(id);
        public async Task<Test?> GetWithQuestionsAsync(int id) => await _testRepo.GetWithQuestionsAsync(id);

        // Специфичные методы
        public async Task<Test> CreateTestAsync(int moduleId)
        {
            var test = new Test(moduleId);
            await _testRepo.AddAsync(test);
            return test;
        }
    }

}
