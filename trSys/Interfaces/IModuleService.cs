using trSys.DTOs;

namespace trSys.Interfaces;

public interface IModuleService
{
    Task<ModuleDto> CreateModuleAsync(ModuleCreateDto dto);
    Task<ModuleDetailsDto?> GetModuleDetailsAsync(int id);
    Task<ModuleDetailsDto> UpdateModuleAsync(ModuleUpdateDto dto);
}
