using System.Linq.Expressions;

namespace SchoolSystem.Application.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);
        Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter, string? includeProperties = null);
        Task<T?> GetByIdAsync(object id, string? includeProperties = null);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
      
    }
}
