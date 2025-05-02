using System.ComponentModel.DataAnnotations;

namespace trSys.DTOs;

public class LessonCreateDto
{
    [Required, MaxLength(100)]
    public string Title { get; set; }

    [MaxLength(500)]
    public string Description { get; set; }

    [Required]
    public int ModuleId { get; set; }
}

public class LessonDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int ModuleId { get; set; }
}
