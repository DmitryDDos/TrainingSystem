using trSys.DTOs;

namespace trSys.Interfaces;

public interface IAnswerService
{
    Task<AnswerDto> UpdateAnswerAsync(int id, AnswerUpdateDto dto);
}
