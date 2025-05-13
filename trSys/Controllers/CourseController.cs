using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using trSys.DTOs;
using trSys.Interfaces;
using trSys.Models;
using trSys.Services;

namespace trSys.Controllers;

[ApiController]
[Route("api/courses")]
public class CourseController : BaseController<Course>
{
    private readonly ICourseService _service;

    public CourseController(IRepository<Course> repository, ICourseService service) : base(repository)
    {
        _service = service;
    }

    [HttpPost("custom")]
    public async Task<ActionResult<CourseDto>> Create([FromBody] CourseCreateDto dto)
    {
        try
        {
            var result = await _service.CreateCourseAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("details/{id}")]
    public async Task<ActionResult<CourseDetailsDto>> Get(int id)
    {
        var result = await _service.GetCourseDetailsAsync(id);
        return result != null ? Ok(result) : NotFound();
    }
}


