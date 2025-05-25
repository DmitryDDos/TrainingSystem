// using System.ComponentModel.DataAnnotations.Schema;
// using System.ComponentModel.DataAnnotations;

// namespace trSys.Models
// {
//     public class LessonFile 
//     {
//         private LessonFile() { }
//         public LessonFile(int id, string fileName, string fileType, byte[] content, int lessonId)
//         {
//             Id = id;
//             FileName = fileName;
//             FileType = fileType;
//             Content = content;
//             LessonId = lessonId;
//         }

//         [Key]
//         [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//         public int Id { get; private set; }
//         public string FileName { get; private set; }
//         public string FileType { get; private set; } // "image", "video", "presentation", etc.
//         public byte[] Content { get; private set; }
//         public int LessonId { get; private set; }

//         // Навигационное свойство
//         public Lesson? Lesson { get; private set; }
//     }
// }
