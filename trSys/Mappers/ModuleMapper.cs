using trSys.DTOs;
using trSys.Models;

namespace trSys.Mappers;

public static class ModuleMapper
{
    public static ModuleDto ToDto(Module module) => new(
        module.Id,
        module.Title,
        module.Description,
        module.CourseId);

    public static ModuleDetailsDto ToDetailsDto(Module module) => new(
        module.Id,
        module.Title,
        module.Description,
        module.CourseId,
        module.Lessons.Select(LessonMapper.ToDto));
}
