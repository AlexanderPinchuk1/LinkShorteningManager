using System.Linq.Expressions;

namespace LinkShorteningManager.Repositories.Repository.Interfaces
{
    public interface IRepository<T>
    {
        public Task<IList<T>> AllAsync();
        public Task<IList<T>> FindAsync(Expression<Func<T, bool>> predicate);
        public Task AddAsync(T entity);
        public Task UpdateAsync(T entity);
        public Task DeleteAsync(T entity);
    }
}
