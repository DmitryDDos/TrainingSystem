using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace trSys.Models;

public class Answer
{
    private Answer () { }
    public Answer(string answersForQuestions, int questionId, bool isCorrect)
    {
        AnswersForQuestions = answersForQuestions;
        QuestionId = questionId;
        IsCorrect = isCorrect;
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id {get; private set;}
    public string AnswersForQuestions {get; private set;}
    public int QuestionId {get; private set;}
    public bool IsCorrect { get; private set; }
    public Question Questions {get; private set;}
}
