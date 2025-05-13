using Microsoft.AspNetCore.Mvc;
using trSys.DTOs;
using trSys.Interfaces;
using trSys.Models;

namespace trSys.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LessonsController : BaseController<Lesson>
{
    private readonly ILessonService _service;

    public LessonsController(IRepository<Lesson> repository, ILessonService service) : base(repository)
    {
        _service = service;
    }

    [HttpPost("custom")]
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
