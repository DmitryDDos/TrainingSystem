using System.ComponentModel.DataAnnotations;

namespace trSys.DTOs;

public class ModuleCreateDto
{
    [Required, MaxLength(100)]
    public string Title { get; set; }

    [MaxLength(500)]
    public string Description { get; set; }

    [Required]
    public int CourseId { get; set; }
}

public class ModuleDto 
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int CourseId { get; set; }
}