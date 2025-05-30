using trSys.DTOs;

namespace trSys.Interfaces;

public interface ILessonService
{
    //Task<LessonDto> CreateLessonAsync(LessonCreateDto dto);
    //Task<IEnumerable<LessonDto>> GetLessonsByModuleAsync(int moduleId);
    //Task<int> GetLessonByIdAsync(int id);

    //Предложено:
    Task<LessonDto> CreateLessonAsync(LessonCreateDto dto);
    Task<LessonDto> GetLessonByIdAsync(int id);
    Task<IEnumerable<LessonDto>> GetLessonsByModuleAsync(int moduleId);
    Task<LessonDto> UpdateLessonAsync(int id, LessonUpdateDto dto);
}
