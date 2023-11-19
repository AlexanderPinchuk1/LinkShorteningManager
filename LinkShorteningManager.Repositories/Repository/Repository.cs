using LinkShorteningManager.Repositories.Repository.Interfaces;
using NHibernate;
using System.Linq.Expressions;

namespace LinkShorteningManager.Repositories.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ISession _session;
        


        public Repository(ISession session)
        {
            _session = session;
        }

        public async Task<IList<T>> AllAsync()
        {
            return await _session.QueryOver<T>().ListAsync();
        }

        public async Task<IList<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _session.QueryOver<T>().Where(predicate).ListAsync();
        }

        public async Task AddAsync(T entity)
        {
            using ITransaction transaction = _session.BeginTransaction();

            await _session.SaveAsync(entity);
            await transaction.CommitAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            using ITransaction transaction = _session.BeginTransaction();
            
            await _session.UpdateAsync(entity);
            await transaction.CommitAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            using ITransaction transaction = _session.BeginTransaction();
            
            await _session.DeleteAsync(entity);
            await transaction.CommitAsync();
        }
    }
}
