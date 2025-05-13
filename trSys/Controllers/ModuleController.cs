using Microsoft.AspNetCore.Mvc;
using trSys.DTOs;
using trSys.Interfaces;
using trSys.Models;

namespace trSys.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ModulesController : BaseController<Module>
{
    private readonly IModuleService _service;

    public ModulesController(IRepository<Module> repository, IModuleService service) : base(repository)
    {
        _service = service;
    }

    [HttpPost("custom")]
    public async Task<ActionResult<ModuleDto>> Create(ModuleCreateDto dto)
    {
        var result = await _service.CreateModuleAsync(dto);
        return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
    }

    [HttpGet("details/{id}")]
    public async Task<ActionResult<ModuleDetailsDto>> Get(int id)
    {
        var module = await _service.GetModuleDetailsAsync(id);
        return module != null ? Ok(module) : NotFound();
    }
}
