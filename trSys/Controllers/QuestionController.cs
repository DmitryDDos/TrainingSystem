using Microsoft.AspNetCore.Mvc;
using trSys.DTOs;
using trSys.Interfaces;
using trSys.Models;

namespace trSys.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuestionsController : BaseController<Question>
{
    private readonly IQuestionService _service;

    public QuestionsController(IRepository<Question> repository, IQuestionService service) : base(repository)
    {
        _service = service;
    }

    [HttpPost("custom")]
    public async Task<ActionResult<QuestionDto>> Create(QuestionCreateDto dto)
    {
        var result = await _service.CreateQuestionAsync(dto);
        return Created($"api/questions/{result.Id}", result);

    }

    [HttpPut("custom/{id}")]
    public async Task<IActionResult> Update(int id, QuestionUpdateDto dto)
    {
        var result = await _service.UpdateQuestionAsync(id, dto);
        return Ok(result);
    }
}
