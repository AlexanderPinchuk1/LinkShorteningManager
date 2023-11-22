using LinkShorteningManager.Repositories.Repository;

namespace LinkShorteningManager.Repositories.UnitOfWork.Interfaces
{
    public interface IUnitOfWork
    {
        public Task CommitAsync();
        public Repository<T> GetRepository<T>() where T : class;
    }
}
