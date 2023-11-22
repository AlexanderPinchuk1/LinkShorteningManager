using LinkShorteningManager.Repositories.UnitOfWork.Interfaces;

namespace LinkShorteningManager.Repositories.UnitOfWork
{
    public class LinkShorteningManagerUnitOfWork : UnitOfWork, ILinkShorteningManagerUnitOfWork
    {
        private readonly LinkShorteningManagerDbContext _linkShorteningManagerDbContext;
        


        public LinkShorteningManagerUnitOfWork(LinkShorteningManagerDbContext dbContext)
            : base(dbContext)
        {
            _linkShorteningManagerDbContext = dbContext;
        }
    }
}