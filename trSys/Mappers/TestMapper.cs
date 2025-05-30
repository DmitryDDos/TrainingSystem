using trSys.DTOs;
using trSys.Models;

namespace trSys.Mappers;

public static class TestMapper
{
    public static TestDto ToDto(Test test) => new(
        test.Id,
        test.Title,
        test.Description,
        test.ModuleId,
        test.Order);

    public static TestWithQuestionsDto ToDtoWithQuestions(Test test) => new(
    test.Id,
    test.Title,
    test.Description,
    test.ModuleId,
    test.Questions?.Select(QuestionMapper.ToDto) ?? Enumerable.Empty<QuestionDto>());

}