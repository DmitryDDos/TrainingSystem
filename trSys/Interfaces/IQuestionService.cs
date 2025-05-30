using trSys.DTOs;

namespace trSys.Interfaces;

public interface IQuestionService
{
    //Task<QuestionDto> CreateQuestionAsync(QuestionCreateDto dto);
    //Task<QuestionDto> UpdateQuestionAsync(int id, QuestionUpdateDto dto);

    Task<QuestionDto> CreateQuestionAsync(QuestionCreateDto dto);
    Task<QuestionDto> UpdateQuestionAsync(int id, QuestionUpdateDto dto);
    Task<QuestionDto> GetQuestionWithAnswersAsync(int id);
    Task<QuestionDto> UpdateQuestionWithAnswersAsync(int id, QuestionUpdateDto dto);
    Task DeleteQuestionAsync(int id);
    Task<IEnumerable<QuestionDto>> GetQuestionsByTestIdAsync(int testId);
}