using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using trSys.DTOs;
using trSys.Interfaces;
using trSys.Models;

namespace trSys.Controllers;

[Authorize(Roles = "User")]
public class CourseRegistrationController : Controller
{
    private readonly ICourseRegistrationService _service;

    public CourseRegistrationController(ICourseRegistrationService service)
    {
        _service = service;
    }

    // POST: /CourseRegistration/Register
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(int courseId)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        try
        {
            var response = await _service.RegisterAsync(
                userId,
                new RegistrationRequestDto { CourseId = courseId });

            TempData["Success"] = "Вы успешно записаны на курс";
            return RedirectToAction("Details", "Course", new { id = courseId });
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return RedirectToAction("Details", "Course", new { id = courseId });
        }
    }

    // GET: /CourseRegistration/CheckAccess
    [HttpGet]
    public async Task<IActionResult> CheckAccess(int courseId)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var result = await _service.CheckAccessAsync(userId, courseId);
        return Json(result);
    }

    // GET: /CourseRegistration/UserCourses
    [HttpGet]
    public async Task<IActionResult> UserCourses()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var courses = await _service.GetUserCoursesAsync(userId);
        return View(courses);
    }

    // GET: /CourseRegistration/Stats/5
    [HttpGet("Stats/{courseId}")]
    public async Task<IActionResult> Stats(int courseId)
    {
        var count = await _service.GetRegistrationCountAsync(courseId);
        return Json(new { RegistrationsCount = count });
    }
}
