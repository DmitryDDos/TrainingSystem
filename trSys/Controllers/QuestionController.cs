using Microsoft.AspNetCore.Mvc;
using trSys.DTOs;
using trSys.Enums;
using trSys.Interfaces;
using trSys.Models;
using trSys.Services;

namespace trSys.Controllers;

[ApiController]
[Route("api/questions")]
public class QuestionsController : ControllerBase
{
    private readonly IQuestionRepository _repository;

    public QuestionsController(IQuestionRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("by-test/{testId}")]
    public async Task<IActionResult> GetByTestId(int testId)
    {
        var questions = await _repository.GetByTestIdAsync(testId);
        return Ok(questions);
    }
}

