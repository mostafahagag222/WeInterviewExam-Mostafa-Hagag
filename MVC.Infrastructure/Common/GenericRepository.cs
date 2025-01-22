using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MVC.Core.Context;
using MVC.Core.Dtos;
using MVC.Core.Interfaces.Common;

namespace MVC.Infrastructure.Common
{
    public class GenericRepository<T>(OutagesDbContext context) : IGenericRepository<T>
        where T : class
    {
        public async Task AddAsync(T entity)
        {
            await context.Set<T>().AddAsync(entity);
        }

        public void DeleteAsync(T entity)

        {
            context.Set<T>().Remove(entity);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int? id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            context.Set<T>().AddRange(entities);
        }

        public void Update(T entity)
        {
            context.Set<T>().Update(entity);
        }

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
        {
            return await context
                .Set<T>()
                .Where(predicate)
                .AnyAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await context.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public async Task<List<ListDto>> GetAllListAsync()
        {
            var idProperty = typeof(T).GetProperty("Id");
            var nameProperty = typeof(T).GetProperty("Name");

            if (idProperty == null || nameProperty == null)
                throw new InvalidOperationException("Type does not have required Id and Name properties");

            return await context.Set<T>()
                .Select(i => new ListDto
                {
                    Id = (int)idProperty.GetValue(i),
                    Name = (string)nameProperty.GetValue(i)
                })
                .ToListAsync();
        }

        public async Task<List<T>> GetManyAsync(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = context.Set<T>();

            return await query
                .Where(predicate)
                .ToListAsync();
        }
    }
}