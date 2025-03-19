using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace trSys.Models;

public class Answer
{
    private Answer () { }
    public Answer(int id, string answersForQuestions, int questionId )
    {
        Id = id;
        AnswersForQuestions = answersForQuestions;
        QuestionId = questionId;    
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id {get; private set;}
    public string AnswersForQuestions {get; private set;}
    public int QuestionId {get; private set;}

    //навигационное поле
    //public Question? Questions {get; private set;}
    public Question Questions {get; private set;}
}
