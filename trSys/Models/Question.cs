using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace trSys.Models;

public class Question
{
    private Question () {}
    public Question(int id, string quest, int testId)
    {
        Id = id;
        Quest = quest;
        TestId = testId;
    }
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id {get; private set;}
    public string Quest {get; private set;}
    public int TestId {get; private set;}

    //навигационные поля
    public Test? Tests {get; private set;}
    public IEnumerable<Answer> Answers {get; private set;} = new List<Answer>();
}
