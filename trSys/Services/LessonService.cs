using trSys.DTOs;
using trSys.Interfaces;
using trSys.Mappers;
using trSys.Models;

namespace trSys.Services;

public class LessonService : ILessonService
{
    private readonly ILessonRepository _lessonRepo;
    private readonly IModuleRepository _moduleRepo;

    public LessonService(
        ILessonRepository lessonRepo,
        IModuleRepository moduleRepo)
    {
        _lessonRepo = lessonRepo ?? throw new ArgumentNullException(nameof(lessonRepo));
        _moduleRepo = moduleRepo ?? throw new ArgumentNullException(nameof(moduleRepo));
    }

    public async Task<LessonDto> CreateLessonAsync(LessonCreateDto dto)
    {
        if (!await _moduleRepo.ExistsAsync(dto.ModuleId))
            throw new ArgumentException("Module not found");

        var lesson = new Lesson(dto.Title, dto.Description, dto.ModuleId);
        await _lessonRepo.AddAsync(lesson);

        return LessonMapper.ToDto(lesson);
    }

    public async Task<IEnumerable<LessonDto>> GetLessonsByModuleAsync(int moduleId)
    {
        var lessons = await _lessonRepo.GetByModuleIdAsync(moduleId);
        return lessons.Select(LessonMapper.ToDto);
    }
}
