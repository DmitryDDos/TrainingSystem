//using Microsoft.AspNetCore.Mvc;
//using trSys.DTOs;
//using trSys.Interfaces;
//using trSys.Models;

//namespace trSys.Controllers
//{

//    public class TestsController : BaseController<Test>
//    {
//        protected override string EntityName => "Test";

//        private readonly IQuestionRepository _questionRepository;

//        public TestsController(
//            IRepository<Test> repository,
//            IQuestionRepository questionRepository)
//            : base(repository)
//        {
//            _questionRepository = questionRepository;

//            // Настройка перенаправления после удаления
//            RedirectAfterDelete = _ => RedirectToAction(nameof(Index));
//        }

//        // GET: Tests/Details/5
//        [HttpGet("{id}")]
//        public async Task<IActionResult> Details(int id)
//        {
//            var test = await _repository.GetByIdAsync(id);
//            if (test == null) return NotFound();

//            var questions = await _questionRepository.GetQuestionsByTestIdAsync(id);

//            var dto = new TestWithQuestionsDto(
//                test.Id,
//                test.Title,
//                test.Description,
//                test.ModuleId,
//                questions.Select(q => new QuestionDto(q.Id, q.Text, q.TestId))
//            );

//            return View(dto);
//        }

//        // GET: Tests/Create
//        [HttpGet]
//        public IActionResult Create()
//        {
//            return View();
//        }

//        // POST: Tests/Create
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create(TestCreateDto dto)
//        {
//            if (!ModelState.IsValid) return View(dto);

//            var test = new Test
//            {
//                Title = dto.Title,
//                Description = dto.Description,
//                ModuleId = dto.ModuleId
//            };

//            await _repository.AddAsync(test);
//            return RedirectToAction(nameof(Details), new { id = test.Id });
//        }

//        // GET: Tests/Edit/5
//        [HttpGet("{id}")]
//        public async Task<IActionResult> Edit(int id)
//        {
//            var test = await _repository.GetByIdAsync(id);
//            if (test == null) return NotFound();

//            var dto = new TestCreateDto
//            {
//                Title = test.Title,
//                Description = test.Description,
//                ModuleId = test.ModuleId
//            };

//            return View(dto);
//        }

//        // POST: Tests/Edit/5
//        [HttpPost("{id}")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(int id, TestCreateDto dto)
//        {
//            if (!ModelState.IsValid) return View(dto);

//            var test = await _repository.GetByIdAsync(id);
//            if (test == null) return NotFound();

//            test.Title = dto.Title;
//            test.Description = dto.Description;
//            test.ModuleId = dto.ModuleId;

//            await _repository.UpdateAsync(test);
//            return RedirectToAction(nameof(Details), new { id });
//        }

//        // GET: Tests/ManageQuestions/5
//        [HttpGet("{id}")]
//        public async Task<IActionResult> ManageQuestions(int id)
//        {
//            var test = await _repository.GetByIdAsync(id);
//            if (test == null) return NotFound();

//            var questions = await _questionRepository.GetQuestionsByTestIdAsync(id);

//            ViewBag.TestId = id;
//            return View(questions);
//        }
//    }
//} // namespace trSys.Comtroller
