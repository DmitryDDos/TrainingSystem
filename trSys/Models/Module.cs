using System.ComponentModel.DataAnnotations;

namespace trSys.Models;

public class Module
{
    private Module() { } // Для EF Core

    public Module(string title, string description, int courseId)
    {
        Title = title ?? throw new ArgumentNullException(nameof(title));
        Description = description;
        CourseId = courseId;
    }

    [Key]
    public int Id { get; private set; }

    [Required]
    [MaxLength(100)]
    public string Title { get; private set; }

    [MaxLength(500)]
    public string Description { get; private set; }

    [Required]
    public int CourseId { get; private set; }

    // Навигационные свойства
    public Course Course { get; private set; } = null!;
    public ICollection<Lesson> Lessons { get; private set; } = new List<Lesson>();
    public ICollection<Test> Tests { get; private set; } = new List<Test>();

    // Методы для бизнес логики
    public void UpdateCourseId(int courseId) => CourseId = courseId;

}
