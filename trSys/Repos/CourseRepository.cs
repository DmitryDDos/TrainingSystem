using System;
using Microsoft.EntityFrameworkCore;
using trSys.Data;
using trSys.Interfaces;
using trSys.Models;

namespace trSys.Repos
{
public class CourseRepository : BaseRepository<Course>
{
   public CourseRepository(AppDbContext context) : base(context) { }
   
    // МЕТОДЫ ИСПОЛЬЗУЕМЫЕ ТОЛЬКО ДЛЯ Course ...    
}
}