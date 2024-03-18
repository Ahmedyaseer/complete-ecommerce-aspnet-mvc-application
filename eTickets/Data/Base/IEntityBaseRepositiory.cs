using System.Linq.Expressions;

namespace eTickets.Data.Base
{
    public interface IEntityBaseRepositiory<T> where T:class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllIncludeAsync(params Expression<Func<T, object>>[] includeProperties );
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(int id);
    }
}
