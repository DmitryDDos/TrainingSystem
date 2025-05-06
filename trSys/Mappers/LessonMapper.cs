using trSys.DTOs;
using trSys.Models;

namespace trSys.Mappers;

public static class LessonMapper
{
    public static LessonDto ToDto(Lesson lesson) => new(
        lesson.Id,
        lesson.Title,
        lesson.Description,
        lesson.ModuleId);
}
