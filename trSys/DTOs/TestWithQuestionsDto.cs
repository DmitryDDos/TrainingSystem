using trSys.Models;

namespace trSys.DTOs
{
    public class TestWithQuestionsDto
    {
        public Test Test { get; set; }
        public List<Question> Questions { get; set; }
    }
}
