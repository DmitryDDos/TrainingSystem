using Microsoft.AspNetCore.Mvc;
using trSys.DTOs;
using trSys.Interfaces;
using trSys.Models;
using System.Threading.Tasks;

namespace trSys.Controllers
{
    public class LessonsController : Controller
    {
        private readonly ILessonService _lessonService;
        private readonly IFileService _fileService;

        public LessonsController(ILessonService lessonService, IFileService fileService)
        {
            _lessonService = lessonService;
            _fileService = fileService;
        }

        // GET: Lessons/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Lessons/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LessonCreateDto dto)
        {
            if (ModelState.IsValid)
            {
                var result = await _lessonService.CreateLessonAsync(dto);
                return RedirectToAction(nameof(Details), new { id = result.Id });
            }
            return View(dto);
        }

        // GET: Lessons/ByModule/5
        public async Task<IActionResult> ByModule(int moduleId)
        {
            var lessons = await _lessonService.GetLessonsByModuleAsync(moduleId);
            return View(lessons);
        }

        // GET: Lessons/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var lesson = await _lessonService.GetLessonByIdAsync(id);
            if (lesson == null)
            {
                return NotFound();
            }
            return View(lesson);
        }

        // GET: Lessons/Upload
        public IActionResult Upload()
        {
            return View();
        }

        // POST: Lessons/Upload
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(FileUploadDto fileDto)
        {
            try
            {
                var fileId = await _fileService.UploadFileAsync(fileDto);
                return RedirectToAction(nameof(Download), new { id = fileId });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(fileDto);
            }
        }

        // GET: Lessons/Download/5
        public async Task<IActionResult> Download(int id)
        {
            try
            {
                var fileDto = await _fileService.DownloadFileAsync(id);
                return File(fileDto.Data, fileDto.ContentType, fileDto.FileName);
            }
            catch (FileNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}