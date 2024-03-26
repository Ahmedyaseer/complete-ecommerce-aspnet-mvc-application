using eTickets.Data.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace eTickets.Data.Base
{
    public class EntityBaseRepositiory<T> : IEntityBaseRepositiory<T> where T : class
    {
        private readonly AppDbContext _context;

        public EntityBaseRepositiory(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
            
        }
        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public async Task<T> UpdateAsync(T entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }
        /* <Summery>
         * can get set of model with include other models 
         * the function take an expression 
         * this expression impelement function takes the model T and other models can include (array of object) named includeProperties
         * step 1 query = get the model
         * step 2 add on the query the other models using Aggregate 
         * retuen the models tolist
         */
        public async Task<IEnumerable<T>> GetAllIncludeAsync(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            return await query.ToListAsync();

        }
    }
}
