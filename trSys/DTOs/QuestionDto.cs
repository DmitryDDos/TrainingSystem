using System.ComponentModel.DataAnnotations;
using trSys.Enums;

namespace trSys.DTOs;

public record QuestionDto(
    int Id,
    string Text,
    QuestionType Type,
    int TestId,
    IEnumerable<AnswerDto> Answers);

public record QuestionCreateDto(
    [Required][MaxLength(500)] string Text,
    [Required] QuestionType Type,
    [Range(1, int.MaxValue)] int TestId,
    IEnumerable<AnswerCreateDto> Answers);

public record QuestionUpdateDto(
    [MaxLength(500)] string? Text,
    QuestionType? Type);
