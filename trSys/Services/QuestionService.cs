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
        if (dto.Answers == null)
        {
            throw new ArgumentException("Answers cannot be null");
        }

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

    public async Task<QuestionDto> GetQuestionWithAnswersAsync(int id)
    {
        var question = await _questionRepo.GetByIdAsync(id, includeAnswers: true); // Изменяем вызов
        return QuestionMapper.ToDto(question);
    }

    public async Task<QuestionDto> UpdateQuestionWithAnswersAsync(int id, QuestionUpdateDto dto)
    {
        // Загружаем вопрос с ответами
        var question = await _questionRepo.GetByIdAsync(id, includeAnswers: true);

        if (dto.Text != null)
            question.UpdateText(dto.Text);

        if (dto.Type.HasValue)
            question.UpdateType(dto.Type.Value);

        // Обновляем или добавляем ответы
        foreach (var answerDto in dto.Answers)
        {
            if (answerDto.Id.HasValue)
            {
                // Обновление существующего ответа
                var answer = question.Answers.FirstOrDefault(a => a.Id == answerDto.Id.Value);
                if (answer != null)
                {
                    if (answerDto.Text != null) answer.UpdateText(answerDto.Text);
                    if (answerDto.IsCorrect.HasValue) answer.MarkAsCorrect(answerDto.IsCorrect.Value);
                }
            }
            else
            {
                // Добавление нового ответа
                question.AddAnswer(new Answer(
                    answerDto.Text ?? string.Empty,
                    answerDto.IsCorrect ?? false,
                    question.Id));
            }
        }

        // Удаление отсутствующих ответов
        var idsToKeep = dto.Answers.Where(a => a.Id.HasValue).Select(a => a.Id.Value);
        var answersToRemove = question.Answers
            .Where(a => !idsToKeep.Contains(a.Id))
            .ToList();

        foreach (var answer in answersToRemove)
        {
            question.Answers.Remove(answer);
            await _answerRepo.DeleteAsync(answer.Id);
        }

        await _questionRepo.UpdateAsync(question);
        return QuestionMapper.ToDto(question);
    }


    public async Task DeleteQuestionAsync(int id)
    {
        var question = await _questionRepo.GetByIdAsync(id);
        if (question != null)
        {
            // Удаляем связанные ответы
            var answers = await _answerRepo.GetByQuestionIdAsync(id);
            foreach (var answer in answers)
            {
                await _answerRepo.DeleteAsync(answer.Id);
            }

            await _questionRepo.DeleteAsync(id);
        }
    }

    public async Task<IEnumerable<QuestionDto>> GetQuestionsByTestIdAsync(int testId)
    {
        var questions = await _questionRepo.GetByTestIdAsync(testId);
        return questions.Select(QuestionMapper.ToDto);
    }
}