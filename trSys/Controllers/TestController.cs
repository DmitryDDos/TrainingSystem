using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using trSys.DTOs;
using trSys.Interfaces;
using trSys.Models;
using trSys.Services;


namespace trSys.Controllers
{
    [Authorize]
    [Route("Tests")]
    public class TestsController : BaseController<Test>
    {
        private readonly ITestService _testService;
        protected override string EntityName => "Test";

        public TestsController(
            IRepository<Test> repository,
            ITestService testService) : base(repository)
        {
            _testService = testService;
            RedirectAfterDelete = test =>
                RedirectToAction("Details", "Modules", new { id = test.ModuleId });
        }

        // GET: Tests/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var testDto = await _testService.GetTestWithQuestionsAsync(id);
            return testDto != null ? View(testDto) : NotFound();
        }

        // GET: Tests/Create?moduleId=5
        [HttpGet("Create")]
        public IActionResult Create(int moduleId)
        {
            var dto = new TestCreateDto { ModuleId = moduleId };
            return View(dto);
        }

        // POST: Tests/Create
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TestCreateDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            try
            {
                await _testService.CreateTestAsync(dto);
                return RedirectToAction("Details", "Modules", new { id = dto.ModuleId });
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(dto);
            }
        }

        // GET: Tests/Edit/5
        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var testDto = await _testService.GetTestByIdAsync(id);
            if (testDto == null) return NotFound();

            var updateDto = new TestUpdateDto(
                testDto.Id,
                testDto.Title,
                testDto.Description,
                testDto.ModuleId
            );
            return View(updateDto);
        }

        // POST: Tests/Edit/5
        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TestUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            try
            {
                await _testService.UpdateTestAsync(id, dto);
                return RedirectToAction("Details", "Modules", new { id = dto.ModuleId });
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(dto);
            }
        }
    }
}
