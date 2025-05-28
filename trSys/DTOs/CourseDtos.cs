using System.ComponentModel.DataAnnotations;

namespace trSys.DTOs;

public record CourseCreateDto(
    [Required][MaxLength(100)] string Title = "",
    [MaxLength(500)] string Description = "");

public record CourseEditDto(
    int Id,
    string Title,
    string Description);

public record CourseDto(
    int Id,
    string Title,
    string Description);

public record CourseDetailsDto(
    int Id,
    string Title,
    string Description,
    IEnumerable<ModuleDto> Modules);
