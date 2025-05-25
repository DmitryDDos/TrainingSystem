using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using trSys.Interfaces;

namespace trSys.Models;

public class CourseRegistration : IEntity
{
    private CourseRegistration () { }
    public CourseRegistration(int id, int userId, int courseId)
    {
        Id = id;
        UserId = userId;
        CourseId = courseId;
        Date = DateOnly.FromDateTime(DateTime.UtcNow);
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id {get; set;}
    [ForeignKey("User")]
    public int UserId {get; set;}
    [ForeignKey("Course")]
    public int CourseId {get; set;}
    public DateOnly Date {get; set;}

    //навигационные поля
    //public ICollection<User> Users {get; private set;} = new List<User>();
    //public ICollection<Course> Courses {get; private set;} = new List<Course>();

    public User? User {get; private set;}
    public Course? Course {get; private set;}
}
