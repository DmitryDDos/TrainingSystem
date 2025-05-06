using System.ComponentModel.DataAnnotations;
using trSys.Enums;

namespace trSys.Models;

public class Question
{
    private Question() { } // Для EF Core

    public Question(string text, QuestionType type, int testId)
    {
        Text = text ?? throw new ArgumentNullException(nameof(text));
        Type = type;
        TestId = testId;
    }

    [Key]
    public int Id { get; private set; }

    [Required]
    [MaxLength(500)]
    public string Text { get; private set; }

    [Required]
    public QuestionType Type { get; private set; }

    [Required]
    public int TestId { get; private set; }

    // Навигационные свойства
    public Test Test { get; private set; } = null!;
    public ICollection<Answer> Answers { get; private set; } = new List<Answer>();

    // Методы бизнес-логики
    public void AddAnswer(Answer answer)
    {
        if (answer == null) throw new ArgumentNullException(nameof(answer));
        Answers.Add(answer);
    }
    public void UpdateText(string text) => Text = text ?? throw new ArgumentNullException(nameof(text));
    public void UpdateType(QuestionType type) => Type = type;
    public void UpdateTestId(int testId) => TestId = testId;

}
