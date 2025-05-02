using Microsoft.AspNetCore.Mvc;
using trSys.DTOs;
using trSys.Interfaces;
using trSys.Services;
using trSys.Models;

namespace trSys.Controllers;

[ApiController]
[Route("api/modules")]
public class ModulesController : BaseController<Module>
{
    private readonly ILessonRepository _lessonRepository;

    public ModulesController(IRepository<Module> repository, ILessonRepository lessonRepository)
        : base(repository)
    {
        _lessonRepository = lessonRepository;
    }

    [HttpGet("{id}/lessons")]
    public async Task<ActionResult<IEnumerable<Lesson>>> GetLessons(int id)
    {
        var lessons = await _lessonRepository.GetByModuleIdAsync(id);
        return Ok(lessons);
    }
}
