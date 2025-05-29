using System.ComponentModel.DataAnnotations;

namespace trSys.DTOs;

public record LessonDto(
    int Id,
    string Title,
    string Description,
    int ModuleId);

public record LessonCreateDto(
    [Required][MaxLength(100)] string Title = "",
    [MaxLength(500)] string Description = "",
    [Range(1, int.MaxValue)] int ModuleId = 0);

public record LessonUpdateDto(
    int Id,
    [Required][MaxLength(100)] string Title,
    [MaxLength(500)] string Description,
    [Range(1, int.MaxValue)] int ModuleId);