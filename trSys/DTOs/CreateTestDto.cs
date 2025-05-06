using System.ComponentModel.DataAnnotations;

namespace trSys.DTOs
{
    public class CreateTestDto
    {
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public int ModuleId { get; set; }
    }
}
