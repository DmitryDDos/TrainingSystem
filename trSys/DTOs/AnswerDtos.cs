using System.ComponentModel.DataAnnotations;

namespace trSys.DTOs;

public record AnswerDto(
    int Id,
    string Text,
    bool IsCorrect,
    int QuestionId);

public record AnswerCreateDto(
    [Required][MaxLength(300)] string Text,
    [Required] bool IsCorrect);

public record AnswerUpdateDto(
    [MaxLength(300)] string? Text,
    bool? IsCorrect);
