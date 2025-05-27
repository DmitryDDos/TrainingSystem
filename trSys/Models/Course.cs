using System.ComponentModel.DataAnnotations.Schema;
using trSys.Interfaces;

namespace trSys.Models
{
    public class Course : IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? CoverImageId { get; set; }

        // Конструктор для создания нового курса (без ID)
        public Course(string title, string description)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Description = description;
        }

        // Конструктор для загрузки существующего курса
        public Course(int id, string title, string description) : this(title, description)
        {
            Id = id;
        }

        [ForeignKey("CoverImageId")]
        public FileEntity CoverImage { get; set; }
        public ICollection<Module> Modules { get; private set; } = new List<Module>();
        public ICollection<CourseRegistration> Registrations { get; set; } = new List<CourseRegistration>();
    }
}
