using System;
using Microsoft.EntityFrameworkCore;
using trSys.Data;
using trSys.Interfaces;

namespace trSys.Repos;
public class BaseRepository<T> : IRepository<T> where T : class
{
    protected readonly AppDbContext _context;

    public BaseRepository(AppDbContext context) => _context = context;

    public virtual async Task AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public virtual async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
        => await _context.Set<T>().AsNoTracking().ToListAsync();

    public virtual async Task<T> GetByIdAsync(int id)
        => await _context.Set<T>().FindAsync(id);

    public virtual async Task<bool> ExistsAsync(int id)
        => await _context.Set<T>().AnyAsync(e => EF.Property<int>(e, "Id") == id);

    public virtual async Task UpdateAsync(T entity)
    {
        _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync();
    }
}
