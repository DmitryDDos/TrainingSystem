using trSys.DTOs;
using trSys.Models;

namespace trSys.Mappers;

public static class QuestionMapper
{
    public static QuestionDto ToDto(Question question) => new(
        question.Id,
        question.Text,
        question.Type,
        question.TestId,
        question.Answers.Select(AnswerMapper.ToDto));
}
