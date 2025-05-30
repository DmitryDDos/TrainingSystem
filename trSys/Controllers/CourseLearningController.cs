using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using trSys.DTOs;
using trSys.Interfaces;
using trSys.Models;

namespace trSys.Controllers
{
    [Authorize(Roles = "User")]
    public class CourseLearningController : Controller
    {
        private readonly IAnswerRepository _answerRepo;
        private readonly IQuestionRepository _questionRepo;
        private readonly ICourseRepository _courseRepo;
        private readonly IUserProgressService _progressService;
        private readonly ILessonRepository _lessonRepo;
        private readonly ITestRepository _testRepo;
        private readonly IModuleRepository _moduleRepo;
        private readonly ITestService _testService;
        private readonly ILogger<CourseLearningController> _logger;

        public CourseLearningController(
            ICourseRepository courseRepo,
            IUserProgressService progressService,
            IModuleRepository moduleRepo,
            ITestService testService,
            ILogger<CourseLearningController> logger,
            ILessonRepository lessonRepo,
            ITestRepository testRepo,
            IQuestionRepository questionRepo,
            IAnswerRepository answerRepo)
        {
            _courseRepo = courseRepo;
            _progressService = progressService;
            _moduleRepo = moduleRepo;
            _testService = testService;
            _logger = logger;
            _lessonRepo = lessonRepo;
            _testRepo = testRepo;
            _questionRepo = questionRepo;
            _answerRepo = answerRepo;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int courseId, int? moduleId = null)
        {
            try
            {
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                // Проверка доступа
                if (!await _progressService.HasAccessAsync(userId, courseId))
                    return Forbid();

                // Загрузка курса с модулями
                var course = await _courseRepo.GetByIdAsync(courseId);
                var modules = (await _moduleRepo.GetByCourseIdAsync(courseId))
                    .OrderBy(m => m.Order)
                    .ToList();

                // Загрузка прогресса
                var progress = await _progressService.GetProgressAsync(userId, courseId);

                // Определение текущего модуля
                Module currentModule = null;
                if (moduleId.HasValue)
                {
                    currentModule = modules.FirstOrDefault(m => m.Id == moduleId);
                }
                else
                {
                    int nextModuleIndex = progress.CompletedModules;
                    currentModule = nextModuleIndex < modules.Count
                        ? modules[nextModuleIndex]
                        : null;
                }

                // Загрузка контента для текущего модуля
                if (currentModule != null)
                {
                    currentModule.Lessons = (await _lessonRepo.GetByModuleIdAsync(currentModule.Id))
                        .Cast<Lesson>()
                        .OrderBy(l => l.Order)
                        .ToList();

                    currentModule.Tests = (await _testRepo.GetTestsWithQuestionsByModuleIdAsync(currentModule.Id))
                        .ToList();

                    foreach (var test in currentModule.Tests)
                    {
                        test.Questions = (await _questionRepo.GetByTestIdAsync(test.Id))
                            .Cast<Question>()
                            .ToList();

                        foreach (var question in test.Questions)
                        {
                            question.Answers = (await _answerRepo.GetByQuestionIdAsync(question.Id))
                                .ToList();
                        }
                    }
                }

                var model = new CourseLearningVM
                {
                    Course = course,
                    CurrentModule = currentModule,
                    Progress = progress,
                    IsCourseCompleted = progress.CompletedModules >= modules.Count
                };

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка загрузки курса");
                return View("Error");
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CompleteTest(
        int testId,
        int courseId,
        int moduleId,
        Dictionary<int, List<int>> answers)
        {
            try
            {
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                if (!await _progressService.HasAccessAsync(userId, courseId))
                    return Forbid();

                // Оценка теста
                var (isPassed, score) = await _testService.EvaluateTest(testId, answers);

                // Сохраняем результат попытки
                await _progressService.RecordTestAttempt(userId, testId, isPassed, score);

                if (isPassed)
                {
                    // Проверяем все ли тесты модуля пройдены
                    var allTestsPassed = await _progressService.AllModuleTestsPassed(userId, moduleId);

                    if (allTestsPassed)
                    {
                        await _progressService.CompleteModuleAsync(userId, courseId, moduleId);
                    }
                }

                return RedirectToAction("Index", new { courseId, moduleId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка завершения теста");
                return RedirectToAction("Index", new { courseId });
            }
        }

    }
    public class CourseLearningVM
    {
        public Course Course { get; set; }
        public Module CurrentModule { get; set; }
        public UserProgressDto Progress { get; set; }
        public bool IsCourseCompleted { get; set; }
        public List<int> CompletedTests => Progress?.CompletedTests ?? new List<int>();

        // Добавляем явно типизированные коллекции
        public IEnumerable<Lesson> OrderedLessons =>
            CurrentModule?.Lessons?.Cast<Lesson>().OrderBy(l => l.Order)
            ?? Enumerable.Empty<Lesson>();

        public IEnumerable<Test> OrderedTests =>
            CurrentModule?.Tests?.Cast<Test>().OrderBy(t => t.Order)
            ?? Enumerable.Empty<Test>();
    }

}
