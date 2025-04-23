using System;
using System.Net;
using Microsoft.EntityFrameworkCore;
using trSys.Models;

namespace trSys.Data;

public class AppDbContext : DbContext
{
    protected readonly IConfiguration configure;

    public AppDbContext(IConfiguration configuration)
    {
        configure = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(configure.GetConnectionString("WebApiDataBase"));
    }

    public DbSet<Answer> Answers {get;set;}
    public DbSet<Course> Courses {get;set;}
    public DbSet<CourseRegistration> CourseRegistrations {get;set;}
    public DbSet<Lesson> Lessons {get;set;}
    public DbSet<Module> Modules {get;set;}
    public DbSet<Question> Questions {get;set;}
    public DbSet<Test> Tests {get;set;}
    public DbSet<User> Users {get;set;}


    //тут связь многие ко многим надо добавить и UTC или как там для даты
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CourseRegistration>()
            .HasKey(cr => cr.Id);

        // modelBuilder.Entity<CourseRegistration>()
        //     .HasOne(cr => cr.Users)
        //     .WithMany(u => u.)
    }
}
