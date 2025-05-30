using trSys.DTOs;

namespace trSys.Interfaces;

public interface ITestService
{
    Task<TestDto> CreateTestAsync(TestCreateDto dto);
    Task<TestWithQuestionsDto> GetTestWithQuestionsAsync(int id);

    Task<TestDto> GetTestByIdAsync(int id);
    Task<TestDto> UpdateTestAsync(int id, TestUpdateDto dto);
    Task<(bool IsPassed, int Score)> EvaluateTest(int testId, Dictionary<int, List<int>> answers);
}
