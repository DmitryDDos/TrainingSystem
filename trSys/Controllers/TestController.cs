using Microsoft.AspNetCore.Mvc;
using trSys.DTOs;
using trSys.Interfaces;

namespace trSys.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestsController : ControllerBase
{
    private readonly ITestService _service;

    public TestsController(ITestService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult<TestDto>> Create(TestCreateDto dto)
    {
        var result = await _service.CreateTestAsync(dto);
        return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
    }

    [HttpGet("{id}/with-questions")]
    public async Task<ActionResult<TestWithQuestionsDto>> Get(int id)
    {
        var test = await _service.GetTestWithQuestionsAsync(id);
        return test != null ? Ok(test) : NotFound();
    }
}
