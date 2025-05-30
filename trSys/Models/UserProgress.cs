using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using trSys.Interfaces;

namespace trSys.Models;

public class UserProgress : IEntity
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
    public int Id { get; set; }

    [ForeignKey("User")]
    public int UserId { get; set; }

    [ForeignKey("Course")]
    public int CourseId { get; set; }

    public int CompletedModules { get; set; }
    public DateTime LastUpdated { get; set; }
    public List<int> CompletedTests { get; set; } = new List<int>();

    public double ProgressPercentage =>
        Course?.Modules.Count == 0 ? 0 : (CompletedModules * 100.0) / Course!.Modules.Count;

    // Навигационные свойства
    public User? User { get; set; }
    public Course? Course { get; set; }

    public void UpdateProgress(int completedModules)
    {
        CompletedModules = completedModules;
        LastUpdated = DateTime.UtcNow;
    }
}
