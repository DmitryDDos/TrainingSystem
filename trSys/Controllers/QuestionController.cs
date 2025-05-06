using Microsoft.AspNetCore.Mvc;
using trSys.DTOs;
using trSys.Interfaces;

namespace trSys.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuestionsController : ControllerBase
{
    private readonly IQuestionService _service;

    public QuestionsController(IQuestionService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult<QuestionDto>> Create(QuestionCreateDto dto)
    {
        var result = await _service.CreateQuestionAsync(dto);
        return Created($"api/questions/{result.Id}", result);

    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, QuestionUpdateDto dto)
    {
        var result = await _service.UpdateQuestionAsync(id, dto);
        return Ok(result);
    }
}
