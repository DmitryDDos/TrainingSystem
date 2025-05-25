using System.ComponentModel.DataAnnotations;
using trSys.Interfaces;

namespace trSys.Models;

public class Lesson : IEntity
{
    private Lesson() { } // Для EF Core

    public Lesson(string title, string description, int moduleId)
    {
        Title = title ?? throw new ArgumentNullException(nameof(title));
        Description = description;
        ModuleId = moduleId;
    }

    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Title { get; set; }

    [MaxLength(500)]
    public string Description { get; set; }

    [Required]
    public int ModuleId { get; set; }

    // Навигационные свойства
    public Module Module { get; set; } = null!;

    // Бизнес-логика методы
    public void UpdateModuleId(int moduleId) => ModuleId = moduleId;

}
