using trSys.DTOs;
using trSys.Interfaces;
using trSys.Mappers;
using trSys.Models;
using trSys.Repos;

namespace trSys.Services;

public class AnswerService : IAnswerService
{
    private readonly IAnswerRepository _answerRepo;
    private readonly IQuestionRepository _questionRepo;

    public AnswerService(
        IAnswerRepository answerRepo,
        IQuestionRepository questionRepo)
    {
        _answerRepo = answerRepo;
        _questionRepo = questionRepo;
    }

    public async Task<AnswerDto> UpdateAnswerAsync(int id, AnswerUpdateDto dto)
    {
        var answer = await _answerRepo.GetByIdAsync(id);

        if (dto.Text != null) answer.UpdateText(dto.Text);
        if (dto.IsCorrect.HasValue) answer.MarkAsCorrect(dto.IsCorrect.Value);

        await _answerRepo.UpdateAsync(answer);
        return AnswerMapper.ToDto(answer);
    }
}
