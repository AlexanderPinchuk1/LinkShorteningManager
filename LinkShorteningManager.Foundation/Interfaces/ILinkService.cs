using LinkShorteningManager.WebApp.Models;

namespace LinkShorteningManager.Foundation.Interfaces
{
    public interface ILinkService
    {
        public Task<List<Link>> AllAsync();

        public Task AddAsync(Link link);

        public Task UpdateAsync(Link link);

        public Task DeleteAsync(Guid linkId);

        public Task<Link?> GetByIdOrReturnNullAsync(Guid linkId);

        public Task<string?> GetLongUrlByKey(string key);

        public Task IncreaseClickCount(string key);
    }
}
