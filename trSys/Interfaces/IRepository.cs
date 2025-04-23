using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;

namespace trSys.Interfaces;

public interface IRepository<T> where T : class // where T : class - ограничение что T обязательно класс
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);
    //Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> filter = null); // фильтры
    //Task<IEnumerable<T>> GetOrderedAsync(Expression<Func<T, object>> orderBy = null); // фильтры
    //Task<bool> ExistsAsync(int id); //Существование сущности
    //Task<(IEnumerable<T> Items, int TotalCount)> GetPagedAsync(int pageNumber, int pageSize); // пагинация

    /*
    Для методов, которые только читают данные (например, GetAllAsync), 
    используйте AsNoTracking, чтобы избежать ненужного отслеживания сущностей:
    РПимер: return await _context.Entity.AsNoTracking.ToListAsync();
    */

}
