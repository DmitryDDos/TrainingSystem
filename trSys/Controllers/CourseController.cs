using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using trSys.DTOs;
using trSys.Interfaces;
using trSys.Models;
using trSys.Services;

namespace trSys.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CourseController : BaseController<Course>
{
    private readonly IModuleRepository _moduleRepository; // Добавлен новый репозиторий

    public CourseController(IRepository<Course> repository, IModuleRepository moduleRepository)
        : base(repository)
    {
        _moduleRepository = moduleRepository;
    }

    [HttpGet("{id}/modules")]
    public async Task<IActionResult> GetModules(int id)
    {
        var modules = await _moduleRepository.GetByCourseIdAsync(id); // Исправленный метод
        return Ok(modules);
    }
}

