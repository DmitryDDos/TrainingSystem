using System;
using Microsoft.EntityFrameworkCore;
using trSys.Data;
using trSys.Interfaces;
using trSys.Models;

namespace trSys.Repos
{
public class LessonRepository : BaseRepository<Lesson>
{
    public LessonRepository(AppDbContext context) : base(context) { }
    // методы уроков ...
}
}