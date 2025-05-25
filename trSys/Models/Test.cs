using System.ComponentModel.DataAnnotations;
using trSys.Interfaces;

namespace trSys.Models;

public class Test : IEntity
{
    private Test() { } // Для EF Core

    public Test(string title, string description, int moduleId)
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
    public Module Module { get; private set; } = null!;
    public ICollection<Question> Questions { get; private set; } = new List<Question>();
    public void UpdateModuleId(int moduleId) => ModuleId = moduleId;
    public void AddQuestion(Question question)
    {
        if (question == null) throw new ArgumentNullException(nameof(question));
        Questions.Add(question);
    }

}
