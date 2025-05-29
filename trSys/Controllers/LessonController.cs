using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using trSys.DTOs;
using trSys.Interfaces;
using trSys.Models;
using System;
using System.Threading.Tasks;

namespace trSys.Controllers
{
    [Authorize]
    public class LessonsController : BaseController<Lesson>
    {
        private readonly ILessonService _lessonService;
        protected override string EntityName => "Lesson";

        public LessonsController(
            IRepository<Lesson> repository,
            ILessonService lessonService) : base(repository)
        {
            _lessonService = lessonService;
            RedirectAfterDelete = lesson =>
                RedirectToAction("Details", "Modules", new { id = lesson.ModuleId });
        }

        // GET: Lessons/Create?moduleId=5
        [HttpGet]
        public IActionResult Create(int moduleId)
        {
            var dto = new LessonCreateDto { ModuleId = moduleId };
            return View(dto);
        }


        // POST: Lessons/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LessonCreateDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            try
            {
                var result = await _lessonService.CreateLessonAsync(dto);
                return RedirectToAction("Details", "Modules", new { id = dto.ModuleId });
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(dto);
            }
        }

        // GET: Lessons/Details/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var lessonDto = await _lessonService.GetLessonByIdAsync(id);
            return lessonDto != null ? View(lessonDto) : NotFound();
        }

        // GET: Lessons/Edit/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var lessonDto = await _lessonService.GetLessonByIdAsync(id);
            if (lessonDto == null)
                return NotFound();

            var editDto = new LessonUpdateDto(
                lessonDto.Id,
                lessonDto.Title,
                lessonDto.Description,
                lessonDto.ModuleId
            );
            return View(editDto);
        }

        // POST: Lessons/Edit/5
        [HttpPost("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LessonUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            try
            {
                var updatedLesson = await _lessonService.UpdateLessonAsync(id, dto);
                return RedirectToAction("Details", "Modules", new { id = dto.ModuleId });
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(dto);
            }
        }

        // Убраны временно методы Upload/Download
    }

}
