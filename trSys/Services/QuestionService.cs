using trSys.DTOs;
using trSys.Interfaces;
using trSys.Mappers;
using trSys.Models;
using trSys.Repos;

namespace trSys.Services;

public class QuestionService : IQuestionService
{
    private readonly IQuestionRepository _questionRepo;
    private readonly ITestRepository _testRepo;
    private readonly IAnswerRepository _answerRepo;

    public QuestionService(
        IQuestionRepository questionRepo,
        ITestRepository testRepo,
        IAnswerRepository answerRepo)
    {
        _questionRepo = questionRepo;
        _testRepo = testRepo;
        _answerRepo = answerRepo;
    }

    public async Task<QuestionDto> CreateQuestionAsync(QuestionCreateDto dto)
    {
        if (!await _testRepo.ExistsAsync(dto.TestId))
            throw new ArgumentException("Test not found");

        var question = new Question(dto.Text, dto.Type, dto.TestId);

        foreach (var answerDto in dto.Answers)
        {
            question.AddAnswer(new Answer(answerDto.Text, answerDto.IsCorrect, question.Id));
        }

        await _questionRepo.AddAsync(question);
        return QuestionMapper.ToDto(question);
    }

    public async Task<QuestionDto> UpdateQuestionAsync(int id, QuestionUpdateDto dto)
    {
        var question = await _questionRepo.GetByIdAsync(id);

        if (dto.Text != null)
            question.UpdateText(dto.Text);

        if (dto.Type.HasValue)
            question.UpdateType(dto.Type.Value);

        await _questionRepo.UpdateAsync(question);
        return QuestionMapper.ToDto(question);
    }

}
