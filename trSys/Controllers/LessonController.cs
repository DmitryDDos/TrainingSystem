using Microsoft.AspNetCore.Mvc;
using trSys.DTOs;
using trSys.Interfaces;
using trSys.Models;
using trSys.Services;

namespace trSys.Controllers;

[ApiController]
[Route("api/lessons")]
public class LessonsController : BaseController<Lesson>
{
    private readonly ILessonRepository _lessonRepository;

    public LessonsController(IRepository<Lesson> repository, ILessonRepository lessonRepository)
        : base(repository)
    {
        _lessonRepository = lessonRepository;
    }

    [HttpGet("by-module/{moduleId}")]
    public async Task<IActionResult> GetByModuleId(int moduleId)
    {
        var lessons = await _lessonRepository.GetByModuleIdAsync(moduleId);
        return Ok(lessons);
    }
}
