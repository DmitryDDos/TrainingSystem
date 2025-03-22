using System;
using trSys.Data;
using trSys.Interfaces;
using trSys.Models;

namespace trSys.Repos
{
public class TestRepository : BaseRepository<Test>
{
   public TestRepository(AppDbContext context) : base(context) { } 
   // методы для Тестов ..
}
}
