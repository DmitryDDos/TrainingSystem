using Microsoft.EntityFrameworkCore;
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

        int maxOrder = await _moduleRepo.GetMaxOrderForCourseAsync(dto.CourseId);

        var module = new Module(dto.Title, dto.Description, dto.CourseId)
        {
            Order = maxOrder + 1
        };

        await _moduleRepo.AddAsync(module);
        return ModuleMapper.ToDto(module);
    }


    public async Task<ModuleDetailsDto> UpdateModuleAsync(ModuleUpdateDto dto)
    {
        var module = await _moduleRepo.GetByIdAsync(dto.Id);
        if (module == null)
            throw new ArgumentException("Module not found");

        if (!await _courseRepo.ExistsAsync(dto.CourseId))
            throw new ArgumentException("Course not found");

        module.Title = dto.Title;
        module.Description = dto.Description;
        module.CourseId = dto.CourseId;

        await _moduleRepo.UpdateAsync(module);
        return ModuleMapper.ToDetailsDto(module);
    }

    public async Task<ModuleDetailsDto?> GetModuleDetailsAsync(int id)
    {
        var module = await _moduleRepo.GetWithLessonsAsync(id);
        return module != null ? ModuleMapper.ToDetailsDto(module) : null;
    }
}
