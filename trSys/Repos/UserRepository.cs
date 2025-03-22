using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using trSys.Data;
using trSys.Interfaces;
using trSys.Models;

namespace trSys.Repos
{
public class UserRepository : BaseRepository<User>
{
    public UserRepository(AppDbContext context) : base(context) { }
    // методы user
}
}