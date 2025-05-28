using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using trSys.DTOs;
using trSys.Interfaces;
using trSys.Models;

namespace trSys.Controllers;

[Authorize]
[Route("Modules")]
public class ModulesController : BaseController<Module>
{
    protected override string EntityName => "Module";
    private readonly IModuleService _service;
    private readonly ICourseService _courseService;

    public ModulesController(IRepository<Module> repository, IModuleService service, ICourseService courseService)
        : base(repository)
    {
        _service = service;
        _courseService = courseService;
    }

    // GET: Modules/Create
    [HttpGet("Create")]
    public async Task<IActionResult> Create(int courseId)
    {
        // Используем CourseService вместо прямого доступа к репозиторию
        var course = await _courseService.GetCourseDetailsAsync(courseId);
        if (course == null)
            return NotFound("Курс не найден");

        var dto = new ModuleCreateDto { CourseId = courseId };
        ViewData["CourseTitle"] = course.Title;
        return View(dto);
    }

    // POST: Modules/Create
    [HttpPost("Create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ModuleCreateDto dto)
    {
        if (!ModelState.IsValid)
            return View(dto);

        try
        {
            var result = await _service.CreateModuleAsync(dto);
            return RedirectToAction("Details", "Course", new { id = dto.CourseId });
        }
        catch (ArgumentException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View(dto);
        }
    }

    [HttpGet("Edit/{id}")]
    public async Task<IActionResult> Edit(int id)
    {
        var module = await _service.GetModuleDetailsAsync(id);
        if (module == null) return NotFound();

        var updateDto = new ModuleUpdateDto(
            module.Id,
            module.Title,
            module.Description,
            module.CourseId); // Автоматическая подстановка CourseId

        return View(updateDto);
    }

    [HttpPost("Edit/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, ModuleUpdateDto dto)
    {
        if (id != dto.Id) return BadRequest("ID mismatch");

        if (!ModelState.IsValid)
            return View(dto);

        try
        {
            var updatedModule = await _service.UpdateModuleAsync(dto);
            return RedirectToAction("Details", "Course", new { id = dto.CourseId });
        }
        catch (ArgumentException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View(dto);
        }
    }



    // GET: Modules/Details/5
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var module = await _service.GetModuleDetailsAsync(id);
        return module != null ? View(module) : NotFound();
    }
}
