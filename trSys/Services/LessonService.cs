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
        _lessonRepo = lessonRepo;
        _moduleRepo = moduleRepo;
    }

    public async Task<LessonDto> CreateLessonAsync(LessonCreateDto dto)
    {
        if (!await _moduleRepo.ExistsAsync(dto.ModuleId))
            throw new ArgumentException("Module not found");

        var lesson = new Lesson(dto.Title, dto.Description, dto.ModuleId);
        await _lessonRepo.AddAsync(lesson);

        return LessonMapper.ToDto(lesson);
    }

    public async Task<LessonDto> GetLessonByIdAsync(int id) // Обновлённая реализация
    {
        var lesson = await _lessonRepo.GetByIdAsync(id);
        return lesson != null ? LessonMapper.ToDto(lesson) : null;
    }

    public async Task<IEnumerable<LessonDto>> GetLessonsByModuleAsync(int moduleId)
    {
        var lessons = await _lessonRepo.GetByModuleIdAsync(moduleId);
        return lessons.Select(LessonMapper.ToDto);
    }

    public async Task<LessonDto> UpdateLessonAsync(int id, LessonUpdateDto dto) // Новая реализация
    {
        var lesson = await _lessonRepo.GetByIdAsync(id);
        if (lesson == null)
            throw new ArgumentException("Lesson not found");

        if (!await _moduleRepo.ExistsAsync(dto.ModuleId))
            throw new ArgumentException("Module not found");

        lesson.Title = dto.Title;
        lesson.Description = dto.Description;
        lesson.ModuleId = dto.ModuleId;

        await _lessonRepo.UpdateAsync(lesson);
        return LessonMapper.ToDto(lesson);
    }
}

