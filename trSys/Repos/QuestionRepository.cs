using System;
using Microsoft.EntityFrameworkCore;
using trSys.Data;
using trSys.Interfaces;
using trSys.Models;

namespace trSys.Repos
{
public class QuestionRepository : BaseRepository<Question>
{
    public QuestionRepository(AppDbContext context) : base(context) { }
    // методы для вопросов?...
    
}
}