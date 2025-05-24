using System;
using trSys.Models;

namespace trSys.Interfaces;

public interface IFileRepository
{
    Task<FileEntity> GetByIdAsync(int id);
    Task<int> AddAsync(FileEntity file);
}
