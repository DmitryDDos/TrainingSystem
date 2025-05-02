using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using trSys.Enums;

namespace trSys.Models;

public class Question
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private set; }

    public string Text { get; private set; } // Текст вопроса
    public QuestionType Type { get; private set; } // Тип вопроса
    public int TestId { get; private set; }

    // Навигационные свойства
    public Test? Test { get; private set; }
    public ICollection<Answer> Answers { get; private set; } = new List<Answer>();

    public Question(string text, QuestionType type, int testId, List<Answer> answers)
    {
        Text = text;
        Type = type;
        TestId = testId;
        Answers = answers;
    }

    private Question() { }
}
