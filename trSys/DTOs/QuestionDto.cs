using trSys.Enums;

namespace trSys.DTOs
{
    public class QuestionDto
    {
        public string Text { get; set; }
        public QuestionType Type { get; set; }
        public int TestId { get; set; }
        public List<AnswerDto> Answers { get; set; } = new();
    }
}
