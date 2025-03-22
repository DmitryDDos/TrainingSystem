using System;
using trSys.Models;
using trSys.Data;
using trSys.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace trSys.Repos
{
public class ModuleRepository : BaseRepository<Module>
{
    public ModuleRepository(AppDbContext context) : base(context) { }
    // методы модулей ...
}
}