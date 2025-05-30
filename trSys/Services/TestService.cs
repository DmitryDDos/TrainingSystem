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

    public async Task<TestDto> GetTestByIdAsync(int id)
    {
        var test = await _testRepo.GetByIdAsync(id);
        return test != null ? TestMapper.ToDto(test) : null;
    }

    public async Task<TestDto> UpdateTestAsync(int id, TestUpdateDto dto)
    {
        var test = await _testRepo.GetByIdAsync(id);
        if (test == null)
            throw new ArgumentException("Test not found");

        if (!await _moduleRepo.ExistsAsync(dto.ModuleId))
            throw new ArgumentException("Module not found");

        test.Title = dto.Title;
        test.Description = dto.Description;
        test.ModuleId = dto.ModuleId;

        await _testRepo.UpdateAsync(test);
        return TestMapper.ToDto(test);
    }

}