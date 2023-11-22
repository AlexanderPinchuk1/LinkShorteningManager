using LinkShorteningManager.Foundation.Interfaces;
using LinkShorteningManager.Repositories.UnitOfWork.Interfaces;
using LinkShorteningManager.WebApp.Models;

namespace LinkShorteningManager.Foundation
{
    public class LinkService : ILinkService
    {
        private readonly ILinkShorteningManagerUnitOfWork _linkShorteningManagerUnitOfWork;



        public LinkService(ILinkShorteningManagerUnitOfWork linkShorteningManagerUnitOfWork)
        {
            _linkShorteningManagerUnitOfWork = linkShorteningManagerUnitOfWork;
        }



        public async Task AddAsync(Link link)
        {
            _linkShorteningManagerUnitOfWork
                .GetRepository<Link>()
                .Add(link);

            await _linkShorteningManagerUnitOfWork
                .CommitAsync();
        }

        public async Task DeleteAsync(Guid linkId)
        {
            var link = (await _linkShorteningManagerUnitOfWork
                .GetRepository<Link>()
                .FindAsync(link => link.Id == linkId))
                .FirstOrDefault();

            if(link == null)
            {
                return;
            }

            _linkShorteningManagerUnitOfWork
                .GetRepository<Link>()
                .Delete(link);

            await _linkShorteningManagerUnitOfWork
                .CommitAsync();
        }

        public async Task<List<Link>> AllAsync()
        {
            return (await _linkShorteningManagerUnitOfWork
                .GetRepository<Link>()
                .AllAsync())
                .ToList();
        }

        public async Task UpdateAsync(Link link)
        {
            _linkShorteningManagerUnitOfWork
                .GetRepository<Link>()
                .Update(link);

            await _linkShorteningManagerUnitOfWork
                .CommitAsync();
        }

        public async Task<Link?> GetByIdOrReturnNullAsync(Guid linkId)
        {
            return (await _linkShorteningManagerUnitOfWork
                .GetRepository<Link>()
                .FindAsync(link => link.Id == linkId))
                .FirstOrDefault();
        }

        public async Task<string?> GetLongUrlByKey(string key)
        {
            return (await _linkShorteningManagerUnitOfWork
                .GetRepository<Link>()
                .FindAsync(link => link.ShortKey == key))
                .FirstOrDefault()
                ?.Url;
        }

        public async Task IncreaseClickCount(string key)
        {
            var link = (await _linkShorteningManagerUnitOfWork
                .GetRepository<Link>()
                .FindAsync(link => link.ShortKey == key))
                .FirstOrDefault();
            
            if(link == null)
            {
                return;
            }

            link.ClickCount++;

            await UpdateAsync(link);
        }
    }
}
