using Microsoft.AspNetCore.Mvc;
using trSys.DTOs;
using trSys.Interfaces;

namespace trSys.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FileController : ControllerBase
{
    private readonly IFileService _fileService;

    public FileController(IFileService fileService)
    {
        _fileService = fileService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetFile(int id)
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
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadFile([FromForm] FileUploadDto fileDto)
    {
        try
        {
            if (fileDto.File == null || fileDto.File.Length == 0)
                return BadRequest("Файл не выбран");

            var fileId = await _fileService.UploadFileAsync(fileDto);
            return Ok(new { FileId = fileId });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}