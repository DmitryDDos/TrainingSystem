using trSys.DTOs;

namespace trSys.Interfaces;

public interface IModuleService
{
    Task<ModuleDto> CreateModuleAsync(ModuleCreateDto dto);
    Task<ModuleDetailsDto?> GetModuleDetailsAsync(int id);
}
