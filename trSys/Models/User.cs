using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;

namespace trSys.Models;

public class User
{
    private User () { }
    public User(string email, string pass, string name, string role)
    {
        Email = email;
        PasswordHash = pass;
        FullName = name;
        Role = role;
        //валидация отдельным методом или Required
    }
    
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id {get; private set;}
    public string Email {get; private set;}
    public string PasswordHash {get; private set;}
    public string FullName {get; private set;}
    public string Role {get; private set;}

    //навигационное поле
    //public CourseRegistration? CourseRegistration {get; private set;}
    public ICollection<CourseRegistration> CourseRegistrations { get; private set; } = new List<CourseRegistration>();

}
