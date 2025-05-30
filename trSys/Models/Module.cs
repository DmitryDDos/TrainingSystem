using System.ComponentModel.DataAnnotations;
using trSys.Interfaces;

namespace trSys.Models;

public class Module : IEntity
{
    private Module() { } // Для EF Core

    public Module(string title, string description, int courseId)
    {
        Title = title ?? throw new ArgumentNullException(nameof(title));
        Description = description;
        CourseId = courseId;
    }

    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Title { get; set; }

    [MaxLength(500)]
    public string Description { get; set; }

    [Required]
    public int CourseId { get; set; }
    [Required]
    [Range(1, int.MaxValue)]
    public int Order { get; set; } = 1;

    // Навигационные свойства
    public Course Course { get; set; } = null!;
    public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
    public ICollection<Test> Tests { get; set; } = new List<Test>();

    // Методы для бизнес логики
    public void UpdateCourseId(int courseId) => CourseId = courseId;

}
