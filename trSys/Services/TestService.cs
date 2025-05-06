using trSys.DTOs;
using trSys.Interfaces;
using trSys.Mappers;
using trSys.Models;

namespace trSys.Services;

public class TestService : ITestService
{
    private readonly ITestRepository _testRepo;
    private readonly IModuleRepository _moduleRepo;

    public TestService(
        ITestRepository testRepo,
        IModuleRepository moduleRepo)
    {
        _testRepo = testRepo ?? throw new ArgumentNullException(nameof(testRepo));
        _moduleRepo = moduleRepo ?? throw new ArgumentNullException(nameof(moduleRepo));
    }

    public async Task<TestDto> CreateTestAsync(TestCreateDto dto)
    {
        if (!await _moduleRepo.ExistsAsync(dto.ModuleId))
            throw new ArgumentException("Module not found");

        var test = new Test(dto.Title, dto.Description, dto.ModuleId);
        await _testRepo.AddAsync(test);

        return TestMapper.ToDto(test);
    }

    public async Task<TestWithQuestionsDto> GetTestWithQuestionsAsync(int id)
    {
        var test = await _testRepo.GetWithQuestionsAsync(id);
        return TestMapper.ToDtoWithQuestions(test);
    }

}
