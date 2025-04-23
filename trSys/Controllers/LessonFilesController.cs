using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using trSys.Interfaces;
using trSys.Models;

namespace trSys.Controllers
{
    [ApiController]
    [Route("api/lessons/{lessonId}/[controller]")]
    public class LessonFilesController : ControllerBase
    {
        private readonly IRepository<Lesson> _lessonRepository;
        private readonly IRepository<LessonFile> _fileRepository;

        public LessonFilesController(
            IRepository<Lesson> lessonRepository,
            IRepository<LessonFile> fileRepository)
        {
            _lessonRepository = lessonRepository;
            _fileRepository = fileRepository;
        }

        // GET: api/lessons/1/files - просмотр файлов урока (доступно всем авторизованным)
        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<IEnumerable<LessonFile>>> GetLessonFiles(int lessonId)
        {
            var lesson = await _lessonRepository.GetByIdAsync(lessonId);
            if (lesson == null)
                return NotFound();

            return Ok(lesson.Files);
        }

        // GET: api/lessons/1/files/5 - скачивание файла (доступно всем авторизованным)
        [HttpGet("{fileId}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> DownloadFile(int lessonId, int fileId)
        {
            var file = await _fileRepository.GetByIdAsync(fileId);
            if (file == null || file.LessonId != lessonId)
                return NotFound();

            return File(file.Content, GetMimeType(file.FileType), file.FileName);
        }

        // POST: api/lessons/1/files - добавление файла (только для админа)
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<LessonFile>> UploadFile(
            int lessonId,
            [FromForm] IFormFile file,
            [FromForm] string fileType)
        {
            var lesson = await _lessonRepository.GetByIdAsync(lessonId);
            if (lesson == null)
                return NotFound();

            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);

            var lessonFile = new LessonFile(
                id: 0,
                fileName: file.FileName,
                fileType: fileType,
                content: memoryStream.ToArray(),
                lessonId: lessonId);

            lesson.AddFile(lessonFile);
            await _fileRepository.AddAsync(lessonFile);
            await _lessonRepository.UpdateAsync(lesson);

            return CreatedAtAction(
                nameof(DownloadFile),
                new { lessonId, fileId = lessonFile.Id },
                lessonFile);
        }

        // DELETE: api/lessons/1/files/5 - удаление файла (только для админа)
        [HttpDelete("{fileId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteFile(int lessonId, int fileId)
        {
            var file = await _fileRepository.GetByIdAsync(fileId);
            if (file == null || file.LessonId != lessonId)
                return NotFound();

            await _fileRepository.DeleteAsync(fileId);
            return NoContent();
        }

        private string GetMimeType(string fileType)
        {
            return fileType switch
            {
                "image" => "image/jpeg",
                "video" => "video/mp4",
                "presentation" => "application/vnd.ms-powerpoint",
                _ => "application/octet-stream"
            };
        }
    }
}
