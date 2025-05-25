using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using trSys.DTOs;
using trSys.Interfaces;
using trSys.Models;
using trSys.Services;

namespace trSys.Controllers
{
    [Authorize]
    public class CourseController : BaseController<Course>
    {
        private readonly ICourseService _service;

        public CourseController(IRepository<Course> repository, ICourseService service) : base(repository)
        {
            _service = service;
        }

        // GET: /Course/CustomCreate
        [HttpGet("CustomCreate")]
        public IActionResult CustomCreate()
        {
            return View();
        }

        // POST: /Course/CustomCreate
        [HttpPost("CustomCreate")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CustomCreate(CourseCreateDto dto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _service.CreateCourseAsync(dto);
                    return RedirectToAction(nameof(Details), new { id = result.Id });
                }
                catch (ArgumentException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(dto);
        }

        // GET: /Course/DetailsExtended/5
        [HttpGet("DetailsExtended/{id}")]
        public async Task<IActionResult> DetailsExtended(int id)
        {
            var result = await _service.GetCourseDetailsAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return View(result);
        }
    }
}
