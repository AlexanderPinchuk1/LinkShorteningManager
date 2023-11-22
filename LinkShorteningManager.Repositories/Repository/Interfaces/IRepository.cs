using System.Linq.Expressions;

namespace LinkShorteningManager.Repositories.Repository.Interfaces
{
    public interface IRepository<T>
    {
        public Task<IEnumerable<T>> AllAsync();
        public Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        public void Add(T entity);
        public void Update(T entity);
        public void Delete(T entity);
    }
}
