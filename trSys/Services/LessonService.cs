using trSys.DTOs;
using trSys.Interfaces;
using trSys.Models;

namespace trSys.Services;

public class LessonService : ILessonRepository
{
    private readonly ILessonRepository _repository;
    private readonly IModuleRepository _moduleRepo;

    public LessonService(ILessonRepository repository, IModuleRepository moduleRepo)
    {
        _repository = repository;
        _moduleRepo = moduleRepo;
    }

    // Реализация ILessonRepository
    public async Task<IEnumerable<Lesson>> GetAllAsync() => await _repository.GetAllAsync();
    public async Task<IEnumerable<Lesson>> GetByModuleIdAsync(int moduleId) => await _repository.GetByModuleIdAsync(moduleId);
    public async Task<Lesson> GetByIdAsync(int id) => await _repository.GetByIdAsync(id);
    public async Task AddAsync(Lesson entity) => await _repository.AddAsync(entity);
    public async Task UpdateAsync(Lesson entity) => await _repository.UpdateAsync(entity);
    public async Task DeleteAsync(int id) => await _repository.DeleteAsync(id);

    // DTO методы
    public async Task<LessonDto> CreateFromDtoAsync(LessonCreateDto dto)
    {
        if (!await _moduleRepo.ExistsAsync(dto.ModuleId))
            throw new ArgumentException("Module not found");

        var lesson = new Lesson(0, dto.Title, dto.Description, dto.ModuleId, new List<LessonFile>());
        await _repository.AddAsync(lesson);
        return ToDto(lesson);
    }

    private static LessonDto ToDto(Lesson lesson) => new()
    {
        Id = lesson.Id,
        Title = lesson.Title,
        Description = lesson.Description,
        ModuleId = lesson.ModuleId
    };
}

