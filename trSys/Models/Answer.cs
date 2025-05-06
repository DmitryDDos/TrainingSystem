using System.ComponentModel.DataAnnotations;

namespace trSys.Models;

public class Answer
{
    private Answer() { } // Для EF Core

    public Answer(string text, bool isCorrect, int questionId)
    {
        Text = text ?? throw new ArgumentNullException(nameof(text));
        IsCorrect = isCorrect;
        QuestionId = questionId;
    }

    [Key]
    public int Id { get; private set; }

    [Required]
    [MaxLength(300)]
    public string Text { get; private set; }

    [Required]
    public bool IsCorrect { get; private set; }

    [Required]
    public int QuestionId { get; private set; }

    // Навигационные свойства
    public Question Question { get; private set; } = null!;

    // Методы бизнес-логики
    public void UpdateQuestionId(int questionId) => QuestionId = questionId;
    public void UpdateText(string text) => Text = text ?? throw new ArgumentNullException(nameof(text));
    public void MarkAsCorrect(bool isCorrect) => IsCorrect = isCorrect;
}
