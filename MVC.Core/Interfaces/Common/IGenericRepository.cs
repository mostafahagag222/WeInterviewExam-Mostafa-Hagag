using System.Linq.Expressions;
using MVC.Core.Dtos;

namespace MVC.Core.Interfaces.Common
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int? id);
        Task<List<T>> GetAllAsync();
        Task AddAsync(T entity);
        void Update(T entity);
        void DeleteAsync(T entity);
        void AddRange(IEnumerable<T> entities);
        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
        Task<T> GetAsync(Expression<Func<T, bool>> predicate);
        Task<List<ListDto>> GetAllListAsync();
        Task<List<T>> GetManyAsync(Expression<Func<T, bool>> predicate);
    }
}