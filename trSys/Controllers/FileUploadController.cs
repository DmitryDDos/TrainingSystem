namespace trSys.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Http;

    [ApiController]
    [Route("api/[controller]")]
    public class FileUploadController : ControllerBase
    {
        [HttpPost("upload")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadFile(
            [FromForm] IFormFile file,
            [FromForm] string description = null)
        {
            // 2.1 Валидация файла
            if (file == null || file.Length == 0)
                return BadRequest("Файл не был загружен");

            if (file.Length > 10 * 1024 * 1024) // 10 MB лимит
                return BadRequest("Файл слишком большой");

            // 2.2 Проверка расширения
            var validExtensions = new[] { ".jpg", ".png", ".gif" };
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (!validExtensions.Contains(extension))
                return BadRequest("Недопустимый формат файла");

            // 2.3 Сохранение файла
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = Guid.NewGuid() + extension;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // 2.4 Возврат результата
            return Ok(new
            {
                OriginalName = file.FileName,
                SavedName = uniqueFileName,
                Size = file.Length,
                Description = description,
                Path = $"/uploads/{uniqueFileName}"
            });
        }

    }
}
