using System.ComponentModel.DataAnnotations;

namespace trSys.DTOs;

public record ModuleDto(
    int Id,
    string Title,
    string Description,
    int CourseId);

public record ModuleUpdateDto(
    int Id,
    [Required][MaxLength(100)] string Title,
    [MaxLength(500)] string Description,
    int CourseId);

public record ModuleDetailsDto(
    int Id,
    string Title,
    string Description,
    int CourseId,
    IEnumerable<LessonDto> Lessons,
    IEnumerable<TestDto> Tests);

public record ModuleCreateDto(
    [Required][MaxLength(100)] string Title = "",
    [MaxLength(500)] string Description = "",
    [Range(1, int.MaxValue)] int CourseId = 0
);
