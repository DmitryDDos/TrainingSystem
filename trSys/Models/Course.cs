using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace trSys.Models;

public class Course
{
    private Course () { }

    public Course(int id, string title, string descr)
    {
        Id = id;
        Title = title;
        Descriptions = descr;
        //валидация
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id {get; private set;}
    public string Title {get; private set;}
    public string Descriptions {get; private set;}

    //Навигационное поле Регистрации
    public Module? Module {get; private set;}
    //public CourseRegistration? CourseRegistration {get; private set;}
    public IEnumerable<CourseRegistration> CourseRegistrations {get;private set;} = new List<CourseRegistration>();

}
