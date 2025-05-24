using System;
using trSys.Data;
using trSys.Interfaces;
using trSys.Models;

namespace trSys.Repos;

public class FileRepository : IFileRepository
{
    private readonly AppDbContext _context;

    public FileRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<FileEntity> GetByIdAsync(int id)
    {
        return await _context.Files.FindAsync(id);
    }

    public async Task<int> AddAsync(FileEntity file)
    {
        _context.Files.Add(file);
        await _context.SaveChangesAsync();
        return file.Id;
    }
}
