using System;
using Microsoft.EntityFrameworkCore;
using trSys.Data;
using trSys.Interfaces;
using trSys.Models;

namespace trSys.Repos
{
public class AnswerRepository : BaseRepository<Answer>
{
    public AnswerRepository(AppDbContext context) : base(context) { }

    // МЕТОДЫ ИСПОЛЬЗУЕМЫЕ ТОЛЬКО ДЛЯ ANSWER ...

}
}