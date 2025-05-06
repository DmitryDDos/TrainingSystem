using Microsoft.AspNetCore.Mvc;
using trSys.DTOs;
using trSys.Interfaces;

namespace trSys.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ModulesController : ControllerBase
{
    private readonly IModuleService _service;

    public ModulesController(IModuleService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult<ModuleDto>> Create(ModuleCreateDto dto)
    {
        var result = await _service.CreateModuleAsync(dto);
        return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ModuleDetailsDto>> Get(int id)
    {
        var module = await _service.GetModuleDetailsAsync(id);
        return module != null ? Ok(module) : NotFound();
    }
}
