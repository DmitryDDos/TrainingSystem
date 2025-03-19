using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace trSys.Models;

public class Module
{
    private Module () { }
    public Module(int id, string title, string descr, int courseID)
    {
        Id = id;
        Title = title;
        Descriptions = descr;
        CourseId = courseID;
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id {get; private set;}
    public string Title {get; private set;}
    public string Descriptions {get; private set;}
    public int CourseId {get; private set;}
    //навигацонные поля
    public Course Course {get; private set;} //а не нужно ли эти поля создовать в конструкторах? а не напримую приравнивать к пустому списску?
    public IEnumerable<Test> Tests {get; private set;} = new List<Test>();
    public IEnumerable<Lesson> Lessons {get; private set;} = new List<Lesson>();

}
