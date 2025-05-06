using trSys.DTOs;
using trSys.Models;

namespace trSys.Mappers;

public static class AnswerMapper
{
    public static AnswerDto ToDto(Answer answer) => new(
        answer.Id,
        answer.Text,
        answer.IsCorrect,
        answer.QuestionId);
}
