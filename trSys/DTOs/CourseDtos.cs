using System.ComponentModel.DataAnnotations;

namespace trSys.DTOs;

public class CourseCreateDto
{
    [Required, MaxLength(100)]
    public string Title { get; set; }

    [MaxLength(500)]
    public string Description { get; set; }
}

public class CourseDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
}
