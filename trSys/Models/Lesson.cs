using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace trSys.Models;

public class Lesson
{
    private Lesson() { }
    public Lesson(int id, string title, string discr, int moduleId, ICollection<LessonFile> lessonFiles)
    {
        Id = id;
        Title = title;
        Description = discr;
        ModuleId = moduleId;
        Files = lessonFiles;
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public int ModuleId { get; private set; }

    //навигационное поле
    public Module? Module { get; private set; }
    public ICollection<LessonFile> Files { get; private set; }

    // Методы для работы с файлами
    public void AddFile(LessonFile file) => Files.Add(file);
    public void RemoveFile(LessonFile file) => Files.Remove(file);
}