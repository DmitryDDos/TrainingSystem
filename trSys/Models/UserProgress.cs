using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace trSys.Models;

public class UserProgress
{
    private UserProgress() { }
    public UserProgress(int userId, int courseId, int completedModules)
    {
        UserId = userId;
        CourseId = courseId;
        CompletedModules = completedModules;
        LastUpdated = DateTime.UtcNow;
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private set; }

    [ForeignKey("User")]
    public int UserId { get; private set; }

    [ForeignKey("Course")]
    public int CourseId { get; private set; }

    public int CompletedModules { get; private set; }
    public DateTime LastUpdated { get; private set; }

    public double ProgressPercentage =>
        Course?.Modules.Count == 0 ? 0 : (CompletedModules * 100.0) / Course!.Modules.Count;

    // Навигационные свойства
    public User? User { get; private set; }
    public Course? Course { get; private set; }

    public void UpdateProgress(int completedModules)
    {
        CompletedModules = completedModules;
        LastUpdated = DateTime.UtcNow;
    }
}
