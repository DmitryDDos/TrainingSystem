using trSys.Enums;
using trSys.Interfaces;
using trSys.Models;
using trSys.Repos;

namespace trSys.Services;

public class QuestionService : IQuestionRepository
{
    private readonly IQuestionRepository _questionRepo;
    private readonly ITestRepository _testRepo;

    public QuestionService(IQuestionRepository questionRepo, ITestRepository testRepo)
    {
        _questionRepo = questionRepo;
        _testRepo = testRepo;
    }

    // Реализация IQuestionRepository
    public async Task<List<Question>> GetByTestIdAsync(int testId)
        => await _questionRepo.GetByTestIdAsync(testId);

    // Добавляем недостающие методы из IRepository<Question>
    public async Task<IEnumerable<Question>> GetAllAsync()
        => await _questionRepo.GetAllAsync();

    public async Task<Question> GetByIdAsync(int id)
        => await _questionRepo.GetByIdAsync(id);

    public async Task AddAsync(Question entity)
        => await _questionRepo.AddAsync(entity);

    public async Task UpdateAsync(Question entity)
        => await _questionRepo.UpdateAsync(entity);

    public async Task DeleteAsync(int id)
        => await _questionRepo.DeleteAsync(id);

    // Специфичные методы сервиса
    public async Task<Question> CreateQuestionAsync(string text, QuestionType type, int testId, List<Answer> answers)
    {
        var question = new Question(text, type, testId, answers);
        await _questionRepo.AddAsync(question);
        return question;
    }
}
