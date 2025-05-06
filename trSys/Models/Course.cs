// Models/Course.cs
namespace trSys.Models
{
    public class Course
    {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }

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
    }
}
