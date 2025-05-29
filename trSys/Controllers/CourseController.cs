using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using trSys.DTOs;
using trSys.Interfaces;
using trSys.Models;
using trSys.Services;

namespace trSys.Controllers;

[Authorize]
public class CourseController : BaseController<Course>
{
    private readonly ICourseService _service;
    protected override string EntityName => "Course";

    public CourseController(IRepository<Course> repository, ICourseService service) : base(repository)
    {
        _service = service;
    }

    // GET: /[controller]
    [HttpGet()]
    public async Task<IActionResult> Index()
    {
        var entities = await _repository.GetAllAsync();
        return View(entities);
    }

    // GET: /Course/Create
    [HttpGet]
    [Authorize(Policy = "AdminOnly")]
    public IActionResult Create()
    {
        return View(new CourseCreateDto());
    }

    // POST: /Course/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> Create(CourseCreateDto dto, IFormFile coverImage)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var result = coverImage != null
                    ? await _service.CreateCourseWithFileAsync(dto, coverImage)
                    : await _service.CreateCourseAsync(dto);

                return RedirectToAction(nameof(Details), new { id = result.Id });
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
        }
        return View(dto);
    }

    // GET: /Course/Details/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Details(int id)
    {
        var result = await _service.GetCourseDetailsAsync(id);
        return result == null ? NotFound() : View(result);
    }

    // GET: /Course/Edit/5
    [HttpGet("{id}")]
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> Edit(int id)
    {
        var course = await _repository.GetByIdAsync(id);
        if (course == null) return NotFound();

        var dto = new CourseEditDto(
            Id: id,
            Title: course.Title ?? string.Empty,
            Description: course.Description ?? string.Empty
        );

        return View(dto);
    }

    // POST: /Course/Edit/5
    [HttpPost("{id}")]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> Edit(int id, CourseEditDto dto)
    {
        if (!ModelState.IsValid)
        {
            return View(dto);
        }

        var course = await _repository.GetByIdAsync(id);
        if (course == null) return NotFound();

        course.Title = dto.Title;
        course.Description = dto.Description;

        try
        {
            await _repository.UpdateAsync(course);
            return RedirectToAction(nameof(Details), new { id });
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _repository.ExistsAsync(id))
                return NotFound();
            throw;
        }

    }
}

