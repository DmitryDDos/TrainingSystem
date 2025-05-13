using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using trSys.DTOs;
using trSys.Interfaces;
using trSys.Models;

namespace trSys.Controllers;

[Authorize(Roles = "User")]
[ApiController]
[Route("api/[controller]")]
public class CourseRegistrationController : BaseController<CourseRegistration>
{
    private readonly ICourseRegistrationService _service;

    public CourseRegistrationController(
        IRepository<CourseRegistration> repository,
        ICourseRegistrationService service) : base(repository)
    {
        _service = service;
    }

    [HttpPost("register")]
    public async Task<ActionResult<RegistrationResponseDto>> Register(
        [FromBody] RegistrationRequestDto request)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        try
        {
            var response = await _service.RegisterAsync(userId, request);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(new AccessCheckDto(false, ex.Message));
        }
    }

    [HttpGet("check-access")]
    public async Task<ActionResult<AccessCheckDto>> CheckAccess(int courseId)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var result = await _service.CheckAccessAsync(userId, courseId);
        return Ok(result);
    }

    [HttpGet("user-courses")]
    public async Task<ActionResult<IEnumerable<UserCourseDto>>> GetUserCourses()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var courses = await _service.GetUserCoursesAsync(userId);
        return Ok(courses);
    }

    [HttpGet("stats/{courseId}")]
    public async Task<IActionResult> GetCourseStats(int courseId)
    {
        var count = await _service.GetRegistrationCountAsync(courseId);
        return Ok(new { RegistrationsCount = count });
    }
}
