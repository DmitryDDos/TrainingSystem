using trSys.DTOs;

namespace trSys.Interfaces;

public interface IQuestionService
{
    Task<QuestionDto> CreateQuestionAsync(QuestionCreateDto dto);
    Task<QuestionDto> UpdateQuestionAsync(int id, QuestionUpdateDto dto);
}
