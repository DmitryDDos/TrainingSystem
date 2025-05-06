using trSys.DTOs;

namespace trSys.Interfaces;

public interface ITestService
{
    Task<TestDto> CreateTestAsync(TestCreateDto dto);
    Task<TestWithQuestionsDto> GetTestWithQuestionsAsync(int id);
}
