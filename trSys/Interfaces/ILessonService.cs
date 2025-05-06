using trSys.DTOs;

namespace trSys.Interfaces;

public interface ILessonService
{
    Task<LessonDto> CreateLessonAsync(LessonCreateDto dto);
    Task<IEnumerable<LessonDto>> GetLessonsByModuleAsync(int moduleId);
}
