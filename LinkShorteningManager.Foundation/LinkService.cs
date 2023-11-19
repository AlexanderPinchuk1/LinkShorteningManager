using LinkShorteningManager.Foundation.Interfaces;
using LinkShorteningManager.Repositories.Repository.Interfaces;
using LinkShorteningManager.WebApp.Models;

namespace LinkShorteningManager.Foundation
{
    public class LinkService : ILinkService
    {
        private readonly IRepository<Link> _linksRepository;



        public LinkService(IRepository<Link> linkRepository)
        {
            _linksRepository = linkRepository;
        }



        public async Task AddAsync(Link link)
        {
            await _linksRepository.AddAsync(link);
        }

        public async Task DeleteAsync(Guid linkId)
        {
            var link = ( await _linksRepository
                .FindAsync(link => link.Id == linkId))
                .FirstOrDefault();

            if(link == null)
            {
                return;
            }

            await _linksRepository.DeleteAsync(link);
        }

        public async Task<List<Link>> AllAsync()
        {
            return (await _linksRepository.AllAsync())
                .ToList();
        }

        public async Task UpdateAsync(Link link)
        {
            await _linksRepository.UpdateAsync(link);
        }

        public async Task<Link?> GetByIdOrReturnNullAsync(Guid linkId)
        {
            return (await _linksRepository.FindAsync(link => link.Id == linkId))
                .FirstOrDefault();
        }

        public async Task<string?> GetLongUrlByKey(string key)
        {
            return (await _linksRepository.FindAsync(link => link.ShortKey == key))
                .FirstOrDefault()
                ?.Url;
        }

        public async Task IncreaseClickCount(string key)
        {
            var link = (await _linksRepository.FindAsync(link => link.ShortKey == key))
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
