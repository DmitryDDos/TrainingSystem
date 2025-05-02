using Microsoft.AspNetCore.Mvc;
using trSys.Models;
using trSys.Services;
using trSys.Interfaces;
using trSys.DTOs;

namespace trSys.Controllers;

[ApiController]
[Route("api/tests")]
public class TestsController : ControllerBase
{
    private readonly ITestRepository _testRepository;
    private readonly IQuestionRepository _questionRepository;

    public TestsController(ITestRepository testRepository, IQuestionRepository questionRepository)
    {
        _testRepository = testRepository;
        _questionRepository = questionRepository;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var test = await _testRepository.GetByIdAsync(id);
        if (test == null) return NotFound();

        var questions = await _questionRepository.GetByTestIdAsync(id);

        return Ok(new TestWithQuestionsDto
        {
            Test = test,
            Questions = questions.ToList()
        });
    }
}

