using System;
using Microsoft.EntityFrameworkCore;
using trSys.Data;
using trSys.Interfaces;
using trSys.Models;

namespace trSys.Repos;

public class UserRepository : IRepository<User>
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User> GetByIdAsync(int id)
    {
        var users = await _context.Users.FindAsync(id);
        if (users != null) return users;
        return null;
    }

    public async Task AddAsync(User entity)
    {
        await _context.Users.AddAsync(entity);
        await _context.SaveChangesAsync();
        //throw new NotImplementedException();
    }

    public async Task UpdateAsync(User entity)
    {
        _context.Users.Update(entity);
        await _context.SaveChangesAsync();
        //throw new NotImplementedException();
    }

    public async Task DeleteAsync(int id)
    {
        var users = await _context.Users.FindAsync(id);
        if(users != null)
        {
            _context.Users.Remove(users);
            await _context.SaveChangesAsync();
        }
        //throw new NotImplementedException();
    }


}
