using Microsoft.AspNetCore.Mvc;
using trSys.DTOs;
using trSys.Interfaces;

namespace trSys.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LessonsController : ControllerBase
{
    private readonly ILessonService _service;

    public LessonsController(ILessonService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult<LessonDto>> Create(LessonCreateDto dto)
    {
        var result = await _service.CreateLessonAsync(dto);
        return CreatedAtAction(nameof(GetByModule), new { moduleId = result.ModuleId }, result);
    }

    [HttpGet("by-module/{moduleId}")]
    public async Task<ActionResult<IEnumerable<LessonDto>>> GetByModule(int moduleId)
    {
        var lessons = await _service.GetLessonsByModuleAsync(moduleId);
        return Ok(lessons);
    }
}
