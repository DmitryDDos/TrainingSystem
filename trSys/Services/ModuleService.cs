using trSys.DTOs;
using trSys.Interfaces;
using trSys.Models;

namespace trSys.Services;

public class ModuleService : IModuleRepository
{
    private readonly IModuleRepository _repository;

    public ModuleService(IModuleRepository repository)
    {
        _repository = repository;
    }

    // Реализация IModuleRepository
    public async Task<IEnumerable<Module>> GetAllAsync() => await _repository.GetAllAsync();
    public async Task<Module> GetByIdAsync(int id) => await _repository.GetByIdAsync(id);
    public async Task AddAsync(Module entity) => await _repository.AddAsync(entity);
    public async Task UpdateAsync(Module entity) => await _repository.UpdateAsync(entity);
    public async Task DeleteAsync(int id) => await _repository.DeleteAsync(id);
    public async Task<IEnumerable<Module>> GetByCourseIdAsync(int courseId) => await _repository.GetByCourseIdAsync(courseId);
    public async Task<bool> ExistsAsync(int id) => await _repository.ExistsAsync(id);

    // DTO методы
    public async Task<ModuleDto> CreateFromDtoAsync(ModuleCreateDto dto)
    {
        var module = new Module(0, dto.Title, dto.Description, dto.CourseId);
        await _repository.AddAsync(module);
        return ToDto(module);
    }

    private static ModuleDto ToDto(Module module) => new()
    {
        Id = module.Id,
        Title = module.Title,
        Description = module.Descriptions,
        CourseId = module.CourseId
    };
}

