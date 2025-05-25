using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;
using trSys.Interfaces;

namespace trSys.Models;

public class User : IEntity
{
    private User () { }
    public User(string email, string pass, string name, string role)
    {
        Email = email;
        PasswordHash = pass;
        FullName = name;
        Role = role;
    }
    
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id {get; set;}
    public string Email {get; set;}
    public string PasswordHash {get; set;}
    public string FullName {get; set;}
    public string Role {get; set;}
    public ICollection<CourseRegistration> CourseRegistrations { get; private set; } = new List<CourseRegistration>();

}
