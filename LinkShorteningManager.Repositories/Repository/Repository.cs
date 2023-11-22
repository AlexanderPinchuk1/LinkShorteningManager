using LinkShorteningManager.Repositories.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LinkShorteningManager.Repositories.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;



        public Repository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }



        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<T>> AllAsync()
        {
            return await _dbSet.ToListAsync<T>();
        }

        public void Add(T entity)
        {
            _context.Entry(entity).State = EntityState.Added;
        }

        public void Delete(T entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
