using System;
using Microsoft.AspNetCore.Mvc;

namespace trSys.Interfaces;

public interface IRepository<T> where T : class // where T : class - ограничение что T обязательно класс
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);

}
