using trSys.DTOs;
using trSys.Interfaces;
using trSys.Mappers;
using trSys.Models;

namespace trSys.Services;

public class ModuleService : IModuleService
{
    private readonly IModuleRepository _moduleRepo;
    private readonly ICourseRepository _courseRepo;

    public ModuleService(
        IModuleRepository moduleRepo,
        ICourseRepository courseRepo)
    {
        _moduleRepo = moduleRepo;
        _courseRepo = courseRepo;
    }

    public async Task<ModuleDto> CreateModuleAsync(ModuleCreateDto dto)
    {
        if (!await _courseRepo.ExistsAsync(dto.CourseId))
            throw new ArgumentException("Course not found");

        var module = new Module(dto.Title, dto.Description, dto.CourseId);
        await _moduleRepo.AddAsync(module);

        return ModuleMapper.ToDto(module);
    }

    public async Task<ModuleDetailsDto?> GetModuleDetailsAsync(int id)
    {
        var module = await _moduleRepo.GetWithLessonsAsync(id);
        return module != null ? ModuleMapper.ToDetailsDto(module) : null;
    }
}
