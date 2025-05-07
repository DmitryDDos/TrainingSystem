using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using trSys.Data;
using trSys.DTOs;
using trSys.Repos;

namespace trSys.Controllers;

[Authorize(Roles = "User")]
[ApiController]
[Route("api/[controller]")]
public class CourseRegistrationController : ControllerBase
{
    private readonly CourseRegistrationRepository _repo;
    private readonly AppDbContext _context;

    public CourseRegistrationController(
        CourseRegistrationRepository repo,
        AppDbContext context)
    {
        _repo = repo;
        _context = context;
    }

    [HttpPost]
    public async Task<ActionResult<RegistrationResponseDto>> Register(
        [FromBody] RegistrationRequestDto request)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        try
        {
            var registration = await _repo.RegisterUser(userId, request.CourseId);
            var course = await _context.Courses.FindAsync(request.CourseId);

            return Ok(new RegistrationResponseDto(
                registration.Id,
                course!.Id,
                course.Title,
                registration.Date
            ));
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
        bool hasAccess = await _repo.HasAccess(userId, courseId);
        return Ok(new AccessCheckDto(hasAccess));
    }

    [HttpGet("user-courses")]
    public async Task<ActionResult<IEnumerable<UserCourseDto>>> GetUserCourses()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var courses = await _repo.GetUserCourses(userId);

        return Ok(courses.Select(c => new UserCourseDto(
            c.Id,
            c.Title,
            c.Description ?? "Описание отсутствует",
            c.Registrations.FirstOrDefault(r => r.UserId == userId)?.Date ?? DateOnly.MinValue
        )));
    }

    [HttpGet("stats/{courseId}")]
    public async Task<IActionResult> GetCourseStats(int courseId)
    {
        var count = await _repo.GetRegistrationCount(courseId);
        return Ok(new { RegistrationsCount = count });
    }
}
