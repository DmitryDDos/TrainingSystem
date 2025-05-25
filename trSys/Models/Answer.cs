using System.ComponentModel.DataAnnotations;
using trSys.Interfaces;

namespace trSys.Models;

public class Answer : IEntity
{
    private Answer() { } // ��� EF Core

    public Answer(string text, bool isCorrect, int questionId)
    {
        Text = text ?? throw new ArgumentNullException(nameof(text));
        IsCorrect = isCorrect;
        QuestionId = questionId;
    }

    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(300)]
    public string Text { get; set; }

    [Required]
    public bool IsCorrect { get; set; }

    [Required]
    public int QuestionId { get; set; }

    // ������������� ��������
    public Question Question { get; set; } = null!;

    // ������ ������-������
    public void UpdateQuestionId(int questionId) => QuestionId = questionId;
    public void UpdateText(string text) => Text = text ?? throw new ArgumentNullException(nameof(text));
    public void MarkAsCorrect(bool isCorrect) => IsCorrect = isCorrect;
}
