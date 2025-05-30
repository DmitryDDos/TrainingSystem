using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using trSys.DTOs;
using trSys.Enums;
using trSys.Interfaces;
using trSys.Services;

namespace trSys.Controllers;

[Authorize]
[Route("Questions")]
public class QuestionsController : Controller
{
    private readonly IQuestionService _questionService;

    public QuestionsController(IQuestionService questionService)
    {
        _questionService = questionService;
    }

    [HttpGet("Create/{testId}")]
    public IActionResult Create(int testId)
    {
        var dto = new QuestionCreateDto(
            Text: "",
            Type: QuestionType.SingleChoice,
            TestId: testId,
            Answers: new List<AnswerCreateDto>()
        );
        return View(dto);
    }

    [HttpPost("Create/{testId}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(int testId, QuestionCreateDto dto)
    {
        var fixedDto = new QuestionCreateDto(
            Text: dto.Text,
            Type: dto.Type,
            TestId: testId,
            Answers: dto.Answers
        );

        if (!ModelState.IsValid) return View(fixedDto);

        try
        {
            await _questionService.CreateQuestionAsync(fixedDto);
            return RedirectToAction("Details", "Tests", new { id = testId });
        }
        catch (ArgumentException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View(fixedDto);
        }
    }


    [HttpGet("Edit/{id}")]
    public async Task<IActionResult> Edit(int id)
    {
        var question = await _questionService.GetQuestionWithAnswersAsync(id);
        if (question == null) return NotFound();

        var answers = question.Answers.Select(a =>
            new AnswerUpdateDto(a.Text, a.IsCorrect, a.Id));

        var updateDto = new QuestionUpdateDto(
            Text: question.Text,
            Type: question.Type,
            Answers: answers
        );

        ViewBag.TestId = question.TestId;
        return View(updateDto);
    }

    [HttpPost("Edit/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, QuestionUpdateDto dto)
    {
        if (!ModelState.IsValid) return View(dto);

        try
        {
            var updated = await _questionService.UpdateQuestionWithAnswersAsync(id, dto);
            return RedirectToAction("Details", "Tests", new { id = updated.TestId });
        }
        catch (ArgumentException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View(dto);
        }
    }
    
    [HttpPost("Delete/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var question = await _questionService.GetQuestionWithAnswersAsync(id);
            if (question == null) return NotFound();

            await _questionService.DeleteQuestionAsync(id);
            return RedirectToAction("Details", "Tests", new { id = question.TestId });
        }
        catch
        {
            return RedirectToAction("Error", "Home");
        }
    }
}
