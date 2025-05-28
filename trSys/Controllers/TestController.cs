//using Microsoft.AspNetCore.Mvc;
//using trSys.DTOs;
//using trSys.Interfaces;
//using trSys.Models;

//namespace trSys.Controllers;

//[ApiController]
//[Route("api/[controller]")]
//public class TestsController : BaseController<Test>
//{
//    private readonly ITestService _service;

//    public TestsController(IRepository<Test> repository, ITestService service) : base(repository)
//    {
//        _service = service;
//    }

//    [HttpPost("custom")]
//    public async Task<ActionResult<TestDto>> Create(TestCreateDto dto)
//    {
//        var result = await _service.CreateTestAsync(dto);
//        return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
//    }

//    [HttpGet("{id}/with-questions")]
//    public async Task<ActionResult<TestWithQuestionsDto>> Get(int id)
//    {
//        var test = await _service.GetTestWithQuestionsAsync(id);
//        return test != null ? Ok(test) : NotFound();
//    }
//}
