using System.ComponentModel.DataAnnotations;

namespace trSys.DTOs;

public record TestDto(
    int Id,
    string Title,
    string Description,
    int ModuleId,
    int Order);

public record TestCreateDto(
    [Required][MaxLength(100)] string Title = "",
    [MaxLength(500)] string Description = "",
    [Range(1, int.MaxValue)] int ModuleId = 1);

public record TestUpdateDto(
    int Id,
    [Required][MaxLength(100)] string Title,
    [MaxLength(500)] string Description,
    [Range(1, int.MaxValue)] int ModuleId);

public record TestWithQuestionsDto(
    int Id,
    string Title,
    string Description,
    int ModuleId,
    IEnumerable<QuestionDto> Questions);

