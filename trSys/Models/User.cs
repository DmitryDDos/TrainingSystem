using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;

namespace trSys.Models;

public class User
{
    private User () { }
    public User(int id, string email, string pass, string name, string role)
    {
        Id = id;
        Email = email;
        PasswordHash = CreateHashPassword(pass);
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
    public IEnumerable<CourseRegistration> Courses {get;private set;} = new List<CourseRegistration>();

    
    
    private string CreateHashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }
}
