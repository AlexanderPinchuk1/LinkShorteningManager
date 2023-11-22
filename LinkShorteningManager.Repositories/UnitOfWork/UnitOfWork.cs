using LinkShorteningManager.Repositories.Repository;
using LinkShorteningManager.Repositories.UnitOfWork.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LinkShorteningManager.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dbContext;
        private Dictionary<string, object>? _repositories;



        public UnitOfWork(DbContext dbContext)
        {
            _dbContext = dbContext;
        }



        public async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public Repository<T> GetRepository<T>() where T : class
        {
            _repositories ??= new Dictionary<string, object>();
            var type = typeof(T).Name;

            if (_repositories.ContainsKey(type))
            {
                return (Repository<T>)_repositories[type];
            }

            var repositoryType = typeof(Repository<>);
            var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _dbContext)
                 ?? throw new Exception("Can't create repositoty instance!");
            _repositories.Add(type, repositoryInstance);

            return (Repository<T>)_repositories[type];
        }
    }
}