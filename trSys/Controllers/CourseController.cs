using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using trSys.DTOs;
using trSys.Interfaces;
using trSys.Models;
using trSys.Services;

namespace trSys.Controllers;

[ApiController]
[Route("api/courses")]
public class CourseController : ControllerBase
{
    private readonly ICourseService _courseService;

    public CourseController(ICourseService courseService)
    {
        _courseService = courseService;
    }

    [HttpPost]
    public async Task<ActionResult<CourseDto>> Create([FromBody] CourseCreateDto dto)
    {
        try
        {
            var result = await _courseService.CreateCourseAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CourseDetailsDto>> Get(int id)
    {
        var result = await _courseService.GetCourseDetailsAsync(id);
        return result != null ? Ok(result) : NotFound();
    }
}


