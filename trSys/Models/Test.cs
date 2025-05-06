using System.ComponentModel.DataAnnotations;

namespace trSys.Models;

public class Test
{
    private Test() { } // Для EF Core

    public Test(string title, string description, int moduleId)
    {
        Title = title ?? throw new ArgumentNullException(nameof(title));
        Description = description;
        ModuleId = moduleId;
    }

    [Key]
    public int Id { get; private set; }

    [Required]
    [MaxLength(100)]
    public string Title { get; private set; }

    [MaxLength(500)]
    public string Description { get; private set; }

    [Required]
    public int ModuleId { get; private set; }

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
