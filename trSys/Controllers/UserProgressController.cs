using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using trSys.DTOs;
using trSys.Interfaces;

namespace trSys.Controllers;

[Authorize(Roles = "User")]
[ApiController]
[Route("api/[controller]")]
public class UserProgressController : ControllerBase
{
    private readonly IUserProgressService _service;

    public UserProgressController(IUserProgressService service)
    {
        _service = service;
    }

    [HttpGet("{courseId}")]
    public async Task<ActionResult<UserProgressDto>> GetProgress(int courseId)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var progress = await _service.GetProgressAsync(userId, courseId);

        return progress == null
            ? NotFound()
            : Ok(progress);
    }

    [HttpPut("{courseId}")]
    public async Task<IActionResult> UpdateProgress(
        int courseId,
        [FromBody] UpdateProgressRequest request)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        try
        {
            await _service.UpdateProgressAsync(userId, courseId, request.CompletedModules);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}

public record UpdateProgressRequest(int CompletedModules);
