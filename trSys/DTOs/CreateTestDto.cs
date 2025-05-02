namespace trSys.DTOs
{
    public class CreateTestDto
    {
        public int ModuleId { get; set; }
        public List<QuestionDto> Questions { get; set; } = new();
    }
}
